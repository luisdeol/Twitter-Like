var ReplyService = (function() {
    var createReply = function(tweetId, replyContent, userId, done, fail) {
        $.ajax({
                url: "/api/replies/",
                type: "POST",
                data: JSON.stringify({ TweetId: tweetId, ReplyContent: replyContent, UserId: userId }),
                contentType: "application/json"
            })
            .done(done)
            .fail(fail);
    };
    return {
        createReply: createReply
    };
})();
