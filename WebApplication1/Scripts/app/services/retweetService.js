var RetweetService = (function() {
    var retweet = function(tweetId, userId, done, fail) {
        $.ajax({
            url: "/api/retweets/",
            type: "POST",
            data: JSON.stringify({ TweetId: tweetId, UserId: userId }),
            contentType: "application/json"
        })
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
})();