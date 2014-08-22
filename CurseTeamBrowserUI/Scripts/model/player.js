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

        self.editGamesPlayedFix = ko.computed(function () {
            if (!parseInt(self.editGamesPlayed()))
                self.editGamesPlayed(0);
            else
                self.editGamesPlayed(parseInt(self.editGamesPlayed()));

            return self.editGamesPlayed();
        });

        self.editGamesWonFix = ko.computed(function () {
            if (parseInt(self.editGamesWon()) > parseInt(self.editGamesPlayed()))
                self.editGamesWon(self.editGamesPlayed());
            else if(!parseInt(self.editGamesWon()))
                self.editGamesWon(0);
            else
                self.editGamesWon(parseInt(self.editGamesWon()));
     

            return self.editGamesWon();
        });

        self.editKillsFix = ko.computed(function () {
            if (!parseInt(self.editKills()) || parseInt(self.editGamesPlayed()) < 1)
                self.editKills(0);
            else
                self.editKills(parseInt(self.editKills()));

            return self.editKills();
        });

        self.editDeathsFix = ko.computed(function () {
            if (!parseInt(self.editDeaths()) || parseInt(self.editGamesPlayed()) < 1)
                self.editDeaths(0);
            else
                self.editDeaths(parseInt(self.editDeaths()));

            return self.editDeaths();
        });

        self.editAssistsFix = ko.computed(function () {
            if (!parseInt(self.editAssists()) || parseInt(self.editGamesPlayed()) < 1)
                self.editAssists(0);
            else
                self.editAssists(parseInt(self.editAssists()));

            return self.editAssists();
        });

        self.edit = function () {
            self.editName(self.name());
            self.editGamesPlayed(self.gamesPlayed() || '0');
            self.editGamesWon(self.gamesWon() || '0');
            self.editKills(self.kills() || '0');
            self.editDeaths(self.deaths() || '0');
            self.editAssists(self.assists() || '0');
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
            player.append('GamesPlayed', self.editGamesPlayed());
            player.append('GamesWon', self.editGamesWon());
            player.append('Kills', self.editKills());
            player.append('Deaths', self.editDeaths());
            player.append('Assists', self.editAssists());
            player.append('IdTeam', self.idTeam());
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