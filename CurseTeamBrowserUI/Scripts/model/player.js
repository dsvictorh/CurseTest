define(['/Scripts/exception.js'], function (exception) {
    var team = function (app, data) {
        //Private access members
        var self = this;
        var ko = app.ko;
        var id = data ? data.Id : null;

        self.name = ko.observable();
        self.gamesPlayed = ko.observable();
        self.gamesWon = ko.observable();
        self.kills = ko.observable();
        self.deaths = ko.observable();
        self.assists = ko.observable();
        self.avatar = ko.observable();
        self.idTeam = ko.observable();
        self.editName = ko.observable();
        self.editGamesPlayed = ko.observable();
        self.editGamesWon = ko.observable();
        self.editKills = ko.observable();
        self.editDeaths = ko.observable();
        self.editAssists = ko.observable();
        self.editting = ko.observable();

        self.getId = function () {
            return id;
        }

        self.edit = function () {
            self.editName(self.name());
            self.editGamesPlayed(self.gamesPlayed());
            self.editGamesWon(self.gamesWon());
            self.editKills(self.kills());
            self.editDeaths(self.deaths());
            self.editAssists(self.assists());
            self.editting(true);
        }

        self.delete = function (success) {
            if(id) {
                $.ajax({
                    url: '/PlayerAdmin/DeletePlayer/',
                    type: 'POST',
                    data: {
                        'id': id,
                        'idTeam': self.idTeam()
                    }
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
            var player = new FormData();
            player.append('Id', id || '');
            player.append('Name', self.editName() || '');
            player.append('GamesPlayed', self.editGamesPlayed() || '0');
            player.append('GamesWon', self.editGamesWon() || '0');
            player.append('Kills', self.editKills() || '0');
            player.append('Deaths', self.editDeaths() || '0');
            player.append('Assists', self.editAssists() || '0');
            player.append('IdTeam', self.idTeam() || '0');
            player.append('ImageUpload', $('#file-upload')[0].files[0] || '');

            $.ajax({
                url: '/PlayerAdmin/SavePlayer',
                type: 'POST',
                processData: false,
                contentType: false,
                cache: false,
                data: player
            }).done(function (data) {
                if (data) {
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

        self.init(data);

    };

    team.prototype.init = function (data) {
        var data = data || {};

        this.name(data.Name);
        this.gamesPlayed(data.GamesPlayed);
        this.gamesWon(data.GamesWon);
        this.kills(data.Kills);
        this.deaths(data.Deaths);
        this.assists(data.Assists);
        this.avatar(data.Avatar + '?' + new Date());
        this.idTeam(data.IdTeam);
        this.editting(false);
    }

    //Public access API
    return {
        create: function (app, data) { return new team(app, data); }
    }
});