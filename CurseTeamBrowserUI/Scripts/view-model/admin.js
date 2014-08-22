define(['/Scripts/model/team.js', '/Scripts/exception.js'], function (team, exception) {
    var viewModel = function (app) {
        //Private access members
        var self = this;
        var ko = app.ko;
        
        //Public access members
        self.teams = ko.observableArray([]);
        self.file = ko.observable();
        self.adding = ko.observable();
        self.addingPlayer = ko.observable();

        self.fileName = ko.computed(function () {
            if (self.file())
                return self.file().substring(self.file().lastIndexOf('\\') != -1 ? self.file().lastIndexOf('\\') + 1 : self.file().lastIndexOf('/') + 1)
            else
                return 'Choose...';
        });

        self.upload = function (team) {
            if(team.editting())
                $('#file-upload').trigger('click');
        };

        self.toggleTeam = function (team) {
            if (!self.adding())
                team.toggle();
        }

        self.listTeams = function () {
            $.ajax({
                url: '/TeamAdmin/ListTeams',
                type: 'POST',
                dataType: 'json'
            }).done(function (data) {
                self.adding(false);
                self.teams([]);
                for (var i = 0; i < data.Teams.length; i++)
                    self.teams.push(team.create(app, data.Teams[i]));
            }).fail(function (xhr, textStatus, errorThrown) {
                alert('An error has occured while listing the teams');
                console.error('Handle error: ' + exception.formatNoHtml(xhr.responseText));
            });
        }

        self.addTeam = function () {
            for (var i = 0; i < self.teams().length; i++) {
                self.teams()[i].editting(false);
            }

            var add = team.create(app)
            self.teams.push(add);

            add.edit();
            self.adding(true);
            self.file('');
        }

        self.editTeam = function (team) {
            for (var i = 0; i < self.teams().length; i++) {
                self.teams()[i].editting(false);
            }

            if (self.adding())
                self.teams.pop();

            team.edit();
            self.adding(false);
            self.file('');
        }

        self.saveTeam = function (team) {
            team.save(self.listTeams);
        }

        self.deleteTeam = function (team) {
            if (confirm('Are you sure you want to delete this team and all it\'s players?'))
                team.delete(self.listTeams);
            self.file('');
        }

        self.cancelTeam = function (team) {
            if (self.adding())
                self.teams.pop();

            team.editting(false);
            self.adding(false);
            self.file('');
        }

        self.init();

    };

    viewModel.prototype.init = function () {
        this.listTeams();
        this.adding(false);
        this.addingPlayer(false);
    }

    //Public access API
    return {
        create: function (app, controller, action) { return new viewModel(app); }
    }
});