var ReportsController = (function() {
    var button;
    var init = function(container) {
        $(container).on('click', '.js-report', report);
    };
    var report = function (e) {
        button = $(e.target);
        bootbox.dialog({
            message: "Are you sure that you want to report "+ button.attr("data-username") + "'s tweet?",
            title: "Confirm",
            buttons: {
                no: {
                    label: 'No',
                    className: 'btn-default',
                    callback: function() {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: 'Yes',
                    className: 'btn-danger',
                    callback: function() {
                        $.post("/api/reports", { TweetId: button.attr("data-tweet-id") })
                            .done(function() {
                                button.parents("tr").fadeOut(function() {
                                    $(this).remove();
                                });
                            })
                            .fail(function() {
                                alert("Something went wrong!");
                            });
                    }
                }
            }
        });

    };
    return {
        init: init
    };
})();