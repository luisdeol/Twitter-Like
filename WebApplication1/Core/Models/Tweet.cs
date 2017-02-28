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

        public int UserId { get; set; }
        public UserProfile User { get; set; }

        public ICollection<Activity> Activities { get; private set; }
        public ICollection<Notification> Notifications { get; set; }

        public Tweet()
        {
            Activities = new Collection<Activity>();
        }

        public Tweet(string content, int userId)
        {
            CreatedAt = DateTime.Now;
            Content = content;
            UserId = userId;
        }
    }
}