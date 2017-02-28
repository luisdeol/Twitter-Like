using System;

namespace WebApplication1.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public DateTime DateTime { get; set; }

        public int ActorId { get; set; }
        public UserProfile Actor { get; set; }

        public int UserId { get; set; }

        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }

        public bool IsRead { get; set; }

        public Notification()
        {

        }

        public Notification(NotificationType type, int actorId, int userId)
        {
            Type = type;
            ActorId = actorId;
            UserId = userId;
            DateTime = DateTime.Now;
        }

        public static Notification Followed(int actor, int userId)
        {
            var notification = new Notification(NotificationType.Followed, actor, userId);
            return notification;
        }

        public static Notification Liked(int actor, int tweetId, int userId)
        {
            var notification = new Notification(NotificationType.Liked, actor, userId) {TweetId = tweetId };

            return notification;
        }

        public static Notification Retweeted(int actor, int tweetId, int userId)
        {
            var notification = new Notification(NotificationType.Retweeted, actor, userId) { TweetId = tweetId };

            return notification;
        }
        public static Notification Replied(int actor, int tweetId, int userId)
        {
            var notification = new Notification(NotificationType.Retweeted, actor, userId) { TweetId = tweetId };

            return notification;
        }
    }
}