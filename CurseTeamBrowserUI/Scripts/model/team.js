define(['/Scripts/model/player.js', '/Scripts/exception.js'], function (player, exception) {
    var team = function (app, data) {
        //Private access members
        var self = this;
        var ko = app.ko;
        var id = data ? data.Id : null;

        //Public access members
        self.name = ko.observable();
        self.avatar = ko.observable();
        self.roster = ko.observableArray([]);
        self.active = ko.observable();
        self.editName = ko.observable();
        self.editting = ko.observable();
        self.addingPlayer = ko.observable();
        self.edittingPlayer = ko.observable();

        self.getId = function () {
            return id;
        }

        self.edit = function () {
            self.editName(self.name());
            self.editting(true);
            self.active(false);
        }

        self.toggle = function () {
            if(!self.editting())
                self.active(!self.active());
        }

        self.delete = function (success) {
            if(id) {
                $.ajax({
                    url: '/TeamAdmin/DeleteTeam/' + id,
                    type: 'POST',
                }).done(function () {
                    if (success)
                        success();
                }).fail(function (xhr, textStatus, errorThrown) {
                    alert('An error has occured while deleting this record');
                    console.error('Handle error: ' + exception.formatNoHtml(xhr.responseText));
                });
            }
        }

        self.save = function (success) {
            var team = new FormData();
            team.append('Id', id || '');
            team.append('Name', self.editName() || '');
            team.append('ImageUpload', $('#file-upload')[0].files[0] || '');

            $.ajax({
                url: '/TeamAdmin/SaveTeam',
                type: 'POST',
                processData: false,
                contentType: false,
                cache: false,
                data: team
            }).done(function (data) {
                if(data){
                    alert(data);
                } else {
                    if (success)
                        success();
                }
                
            }).fail(function (xhr, textStatus, errorThrown) {
                alert('An error has occured while saving this record');
                console.error('Handle error: ' + exception.formatNoHtml(xhr.responseText));
            });
        }

        self.listPlayers = function () {
            if (id) {
                $.ajax({
                    url: '/PlayerAdmin/ListPlayers/',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        'idTeam': id
                    }
                }).done(function (data) {
                    self.addingPlayer(false);
                    self.roster([]);
                    for (var i = 0; i < data.length; i++)
                        self.roster.push(player.create(app, data[i]));
                }).fail(function (xhr, textStatus, errorThrown) {
                    alert('An error has occured while listing the players');
                    console.error('Handle error: ' + exception.formatNoHtml(xhr.responseText));
                });
            }
        }

        self.addPlayer = function () {
            for (var i = 0; i < self.roster().length; i++) {
                self.roster()[i].editting(false);
            }

            var add = player.create(app, {IdTeam: id})
            self.roster.push(add);

            add.edit();
            self.addingPlayer(true);
        }

        self.editPlayer= function (player) {
            for (var i = 0; i < self.roster().length; i++) {
                self.roster()[i].editting(false);
            }

            if (self.addingPlayer())
                self.roster.pop();

            player.edit();
            self.addingPlayer(false);
        }

        self.savePlayer = function (player) {
            player.save(self.listPlayers);
        }

        self.deletePlayer = function (player) {
            if (confirm('Are you sure you want to delete this player?'))
                player.delete(self.listPlayers);
        }

        self.cancelPlayer = function (player) {
            if (self.addingPlayer())
                self.roster.pop();

            player.editting(false);
            self.addingPlayer(false);
        }

        self.init(app, data);

    };

    team.prototype.init = function (app, data) {
        var data = data || {};

        this.name(data.Name);
        this.avatar(data.Avatar + '?' + new Date());
        this.active(false);
        this.editting(false);
        this.addingPlayer(false);
        this.edittingPlayer(false);

        if(data.Roster)
            for (var i = 0; i < data.Roster.length; i++)
                this.roster.push(player.create(app, data.Roster[i]));
    }

    //Public access API
    return {
        create: function (app, data) { return new team(app, data); }
    }
});