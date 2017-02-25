using System;

namespace WebApplication1.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public DateTime DateTime { get; set; }

        public string ActorId { get; set; }
        public ApplicationUser Actor { get; set; }

        public string UserId { get; set; }

        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public bool IsRead { get; set; }

        public Notification()
        {

        }

        public Notification(NotificationType type, string actorId, string userId)
        {
            Type = type;
            ActorId = actorId;
            UserId = userId;
            DateTime = DateTime.Now;
        }

        public static Notification Followed(string actor, string userId)
        {
            var notification = new Notification(NotificationType.Followed, actor, userId);
            return notification;
        }

        public static Notification Liked(string actor, int tweetId, string userId)
        {
            var notification = new Notification(NotificationType.Liked, actor, userId) {TweetId = tweetId };

            return notification;
        }

        public static Notification Retweeted(string actor, int tweetId, string userId)
        {
            var notification = new Notification(NotificationType.Retweeted, actor, userId) { TweetId = tweetId };

            return notification;
        }
        public static Notification Replied(string actor, int tweetId, string userId)
        {
            var notification = new Notification(NotificationType.Retweeted, actor, userId) { TweetId = tweetId };

            return notification;
        }
    }
}