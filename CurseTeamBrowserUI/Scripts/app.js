define(['/Scripts/ko.js'], function (ko) {

    //Prevent KO events bubbling
    ko.bindingHandlers.preventBubble = {
        init: function (element, value) {
            var eventName = ko.utils.unwrapObservable(value());
            ko.utils.registerEventHandler(element, eventName, function (event) {
                event.cancelBubble = true;
                if (event.stopPropagation) {
                    event.stopPropagation();
                }
            });
        }
    };

    //Public access API
    return {
        ko: ko
    };
});