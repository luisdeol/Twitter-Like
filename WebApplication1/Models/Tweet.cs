using System;

namespace WebApplication1.Models
{
    public class Tweet
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Tweet()
        {
            
        }
        public Tweet(string content, string userId)
        {
            CreatedAt = DateTime.Now;
            Content = content;
            UserId = userId;
        }
    }
}