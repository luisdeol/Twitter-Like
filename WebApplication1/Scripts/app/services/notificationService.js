var notificationService = (function () {
    var getNotifications = function () {
        $.getJSON('api/notifications',
            function (notifications) {
                if (notifications.length == 0)
                    return;

                $('.js-notifications-count')
                    .text(notifications.length)
                    .removeClass('hide')
                    .addClass("animated bounceInDown");
            })
    };
    return {
        getNotifications: getNotifications
    }
})();