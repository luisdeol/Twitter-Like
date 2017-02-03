var LikesController = function() {

    var button;

    var init = function(container) {
        $(container).on("click", ".js-like", giveLike);
    };
    var giveLike = function(e) {
        button = $(e.target);
        var tweetId = button.attr("data-tweet-id");
        if (button.hasClass("liked")) {
            $.ajax({
                    url: "/api/likes/" + tweetId ,
                    method: "DELETE"
                })
                .done(function() {
                    button.text("Like").removeClass("liked").addClass("like");
                })
                .fail(function () {
                    alert("Something went wrong");
                });
        } else {
            $.post("/api/likes", { TweetId: tweetId })
                .done(function() {
                    button.text("Liked").removeClass("like").addClass("liked");
                })
                .fail(function() {
                    alert("Something went wrong!");
                });
        }
    }
    return {
        init: init
    }
}();
