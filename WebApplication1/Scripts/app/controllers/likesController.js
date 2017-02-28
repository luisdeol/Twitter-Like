var LikesController = function(likeService) {

    var button;

    var init = function(container) {
        $(container).on("click", ".js-like", giveLike);
    };
    var giveLike = function(e) {
        button = $(e.target);
        var tweetId = button.attr("data-tweet-id");
        var userId = button.attr("data-user-id");
        console.log(userId);
        if (button.hasClass("like"))
            likeService.like(tweetId, userId, done, fail);
        else
            likeService.cancelLike(tweetId, done, fail);
    };

    var done = function() {
        var text = (button.text() == "Liked") ? "Like" : "Liked";
        button.toggleClass("liked").toggleClass("like").text(text);
    };

    var fail = function() {
        alert("Something went wrong!");
    };

    return {
        init: init
    }
}(LikeService);
