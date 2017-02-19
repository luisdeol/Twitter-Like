var FollowingController = function (followingService) {

    var button;

    var init = function(container) {
        $(container).on("click", ".js-follow", follow);
    }

    var follow = function(e) {
        button = $(e.target);
        var followeeId = button.attr("followee-id");
        if (button.hasClass("follow"))
            followingService.follow(followeeId, done, fail);
        else
            followingService.unfollow(followeeId, done, fail);
    }
    var done = function() {
        var text = (button.text() == "Following") ? "Follow" : "Following";
        button.toggleClass("following").toggleClass("follow").text(text);
    }
    var fail = function() {
        alert("Something went wrong!");
    }
    return {
        init: init
    }
}(FollowingService);