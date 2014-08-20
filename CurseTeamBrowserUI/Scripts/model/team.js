define(['/Scripts/exception.js'], function (exception) {
    var team = function (app, data) {
        //Private access members
        var self = this;
        var ko = app.ko;

        self.id = ko.observable();
        self.name = ko.observable();
        self.avatar = ko.observable();
        self.active = ko.observable();
        self.editting = ko.observable();

        self.toggle = function () {
            self.active(!self.active());
        }

        self.delete = function (success) {
            $.ajax({
                url: '/TeamAdmin/DeleteTeam/' + self.id(),
                type: 'POST',
            }).done(function () {
                if(success)
                    success();
            }).fail(function (xhr, textStatus, errorThrown) {
                alert('An error has occured while deleting this record');
                console.error('Handle error: ' + exception.formatNoHtml(xhr.responseText));
            });
        }

        self.init(data);

    };

    team.prototype.init = function (data) {
        this.id(data.Id);
        this.name(data.Name);
        this.avatar(data.Avatar);
        this.active(false);
        this.editting(false);
    }

    //Public access API
    return {
        create: function (app, data) { return new team(app, data); }
    }
});