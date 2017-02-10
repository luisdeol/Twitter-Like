var ReplyController = function(replyService) {
    var button;
    var init = function(container) {
        $(container).on("click", ".js-reply", reply);
    };
    var reply = function(e) {
        button = $(e.target);
        bootbox.dialog({
            title: "Reply to " + button.attr("data-username") + "'s tweet",
            message:
                "<textarea class = 'form-control content' id='reply-content' style='min-width: 100%; resize:none' rows='5'></textarea>",
            buttons: {
                no: {
                    label: "Cancel",
                    className: "btn-default",
                    callback: function() {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: "Reply",
                    className: "btn-info",
                    callback: function () {
                        $.ajax({
                            url: "/api/replies/",
                            type: "POST",
                            data: JSON.stringify({ TweetId: button.attr("data-tweet-id"), ReplyContent: $("#reply-content").val() }),
                            contentType: "application/json"
                        })
                        .done(function(){})
                        .fail(function(){});
                        //replyService.createReply(100,
                        //$("#reply-content").val(), done, fail);
                    }
                    }
                }
        });
    };

    var done = function() {
    };
    var fail = function() {
    };
    return {
        init: init
    };
}(ReplyService);