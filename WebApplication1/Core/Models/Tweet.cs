using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebApplication1.Core.Models
{
    public class Tweet
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Activity> Activities { get; private set; }

        public Tweet()
        {
            Activities = new Collection<Activity>();
        }

        public Tweet(string content, string userId)
        {
            CreatedAt = DateTime.Now;
            Content = content;
            UserId = userId;
        }
    }
}