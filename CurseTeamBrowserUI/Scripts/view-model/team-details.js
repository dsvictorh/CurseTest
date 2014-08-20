define([], function () {
    var viewModel = function (app) {
        //Private access members
        var self = this;
        var ko = app.ko;

    };

    //Public access API
    return {
        create: function (app, controller, action) { return new viewModel(app); }
    }
});