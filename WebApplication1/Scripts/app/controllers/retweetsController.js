var RetweetsController = function(retweetService) {
    var button;

    var init = function(container) {
        $(container).on("click", ".js-retweet", retweet);
    };

    var retweet = function(e) {
        button = $(e.target);
        var tweetId = button.attr("data-tweet-id");
        if (button.hasClass("retweeted"))
            retweetService.cancelRetweet(tweetId, done, fail);
        else
            retweetService.retweet(tweetId, done, fail);
    };

    var done = function() {
        var text = (button.text === "Retweeted") ? "Retweet" : 'Retweeted';
        button.toggleClass("retweeted").toggleClass("retweet").text(text);
    };

    var fail = function() {
        alert("Something went wrong!");
    };

    return {
        init: init
    }
}(RetweetService);