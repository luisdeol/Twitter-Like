using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Like
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TweetId { get; set; }

        public ApplicationUser User { get; set; }
        public Tweet Tweet { get; set; }

        public Like()
        {
            
        }

        public Like(string userId, int tweetId)
        {
            UserId = userId;
            TweetId = tweetId;
        }
    }
}