var RetweetsController = function() {
    var button;

    var init = function(container) {
        $(container).on("click", ".js-retweet", retweet);
    };

    var retweet = function(e) {
        button = $(e.target);
        var tweetId = button.attr("data-tweet-id");
        if (button.hasClass("retweeted")) {
            $.ajax({
                    url: '/api/retweets/' + tweetId,
                    method: 'DELETE'
                })
                .done(function () {
                    button.text("Retweet").removeClass("retweeted").addClass("retweet");;
                })
                .fail(function () {
                    alert("Something went wrong!");
                });
        } else {
            $.post('/api/retweets/', { TweetId: tweetId })
                .done(function () {
                    button.text('Retweeted').removeClass("retweet").addClass("retweeted");;
                })
                .fail(function () {
                    alert("Something went wrong!");
                });
        }
    };
    return {
        init: init
    }
}();