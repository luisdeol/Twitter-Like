var ReportsController = function() {
    var button;
    var init = function(container) {
        $(container).on('click', '.js-report', report);
    }
    var report = function(e) {
        button = $(e.target);
        $.post("/api/reports", { TweetId: button.attr("data-tweet-id") })
            .done(function () {
                button.text("Reported");
            })
            .fail(function () {
                alert("Something went wrong!");
            });
    }
    return {
        init: init
    }
}();