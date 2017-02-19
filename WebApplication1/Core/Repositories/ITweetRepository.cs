using System.Collections.Generic;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface ITweetRepository
    {
        IEnumerable<Tweet> GetNewerTweets(string userId);
        IEnumerable<Tweet> Mine(string userId);
        void AddTweet(string content, string userId);
        List<Tweet> GetTweetsByUsername(string username);
    }
}