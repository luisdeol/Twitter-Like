var RetweetService = function() {
    var retweet = function(tweetId, done, fail) {
        $.post("/api/retweets", { TweetId: tweetId })
            .done(done)
            .fail(fail);
    };
    var cancelRetweet = function(tweetId, done, fail) {
        $.ajax({
                url: "/api/retweets/"+tweetId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };
    return {
        retweet: retweet,
        cancelRetweet: cancelRetweet
    }
}();