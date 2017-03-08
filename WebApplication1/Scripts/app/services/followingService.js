var FollowingService =(function() {
    var follow = function(followeeId, done, fail) {
        $.post("/api/followings/", { FolloweeId: followeeId })
            .done(done)
            .fail(fail);
    }
    var unfollow = function(followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            type: "DELETE"
        })
        .done(done)
        .fail(fail);
    }
    return {
        follow: follow,
        unfollow: unfollow
    }
})();