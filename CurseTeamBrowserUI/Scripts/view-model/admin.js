define(['/Scripts/model/team.js', '/Scripts/exception.js'], function (team, exception) {
    var viewModel = function (app) {
        //Private access members
        var self = this;
        var ko = app.ko;
        
        //Public access members
        self.teams = ko.observableArray([]);
        self.file = ko.observable();

        self.upload = function (team) {
            if(team.editting())
                $('#file-upload').trigger('click');
        };

        self.listTeams = function () {
            $.ajax({
                url: '/TeamAdmin/ListTeams',
                type: 'POST',
                dataType: 'json'
            }).done(function (data) {
                self.teams([]);
                for (var i = 0; i < data.Teams.length; i++)
                    self.teams.push(team.create(app, data.Teams[i]));
            }).fail(function (xhr, textStatus, errorThrown) {
                alert('An error has occured while listing the teams');
                console.error('Handle error: ' + exception.formatNoHtml(xhr.responseText));
            });
        }

        self.editTeam = function (team) {
            for (var i = 0; i < self.teams().length; i++) {
                self.teams()[i].editting(false);
            }

            team.active(false);
            team.editting(true);
            self.file('');
        }

        self.deleteTeam = function (team) {
            if (confirm('Are you sure you want to delete this team and all it\'s players?'))
                team.delete(self.listTeams);
            self.file('');
        }

        self.cancelTeam = function (team) {
            team.editting(false);
            self.file('');
        }

        self.init();

    };

    viewModel.prototype.init = function () {
        this.listTeams();
    }

    //Public access API
    return {
        create: function (app, controller, action) { return new viewModel(app); }
    }
});