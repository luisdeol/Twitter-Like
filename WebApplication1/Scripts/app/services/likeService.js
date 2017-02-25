var LikeService = function () {
    var like = function(tweetId, userId, done, fail) {
        $.ajax({
            url: "/api/likes/",
            type: "POST",
            data: JSON.stringify({ TweetId: tweetId, UserId: userId }),
            contentType: "application/json"
        })
            .done(done)
            .fail(fail);
    };
    var cancelLike = function(tweetId, done, fail) {
        $.ajax({
                url: "/api/likes/" + tweetId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };
    return {
        like: like,
        cancelLike: cancelLike
    };
}();