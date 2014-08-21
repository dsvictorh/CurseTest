define(['/Scripts/exception.js'], function (exception) {
    var team = function (app, data) {
        //Private access members
        var self = this;
        var ko = app.ko;

        self.id = ko.observable();
        self.name = ko.observable();
        self.avatar = ko.observable();
        self.active = ko.observable();
        self.editName = ko.observable();
        self.editting = ko.observable();

        self.edit = function () {
            self.editName(self.name());
            self.editting(true);
            self.active(false);
        }

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

        self.save = function (success) {
            var team = new FormData();
            team.append('Id', self.id() || '');
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

        self.init(data);

    };

    team.prototype.init = function (data) {
        var data = data || {};
       
        this.id(data.Id);
        this.name(data.Name);
        this.avatar(data.Avatar + '?' + new Date());
        this.active(false);
        this.editting(false);
    }

    //Public access API
    return {
        create: function (app, data) { return new team(app, data); }
    }
});