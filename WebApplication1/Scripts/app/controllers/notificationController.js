var NotificationController = (function (notificationService) {
    var init = function () {
        notificationService.getNotifications();
    };
    return {
        init: init
    }
})(notificationService);