var LikesController = function() {

    var button;

    var init = function() {
        $(".js-like").click(giveLike);
    };
    var giveLike = function() {
        $(".js-like").click(function(e) {
            button = $(e.target);
            if (button.hasClass("liked")) {
                $.ajax({
                        url: "/api/likes/" + button.attr("data-tweet-id"),
                        method: "DELETE"
                    })
                    .done(function() {
                        button.text("Like");
                    }).fail(function() {
                        alert("Something went wrong");
                    });
            } else {
                $.post("/api/likes", { TweetId: button.attr("data-tweet-id") })
                    .done(function() {
                        button.text("Liked");
                    })
                    .fail(function() {
                        alert("Something went wrong!");
                    });
            }
        });
    }
    return {
        init: init
    }
}();
