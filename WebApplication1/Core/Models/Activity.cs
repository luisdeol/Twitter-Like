using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Core.Models
{
    public class Activity
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TweetId { get; set; }

        [Key]
        [Column(Order = 3)]
        public ActivityTypes ActivityType { get; set; }

        public string Content { get; set; }

        public ApplicationUser User { get; set; }
        public Tweet Tweet { get; set; }

        public Activity()
        {
            
        }

        public Activity(string userId, int tweetId, ActivityTypes type, string content = "")
        {
            UserId = userId;
            TweetId = tweetId;
            ActivityType = type;
            Content = content;
        }
    }
}