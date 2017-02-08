var LikeService = function () {
    var like = function(tweetId, done, fail) {
        $.post("/api/likes", { TweetId : tweetId })
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