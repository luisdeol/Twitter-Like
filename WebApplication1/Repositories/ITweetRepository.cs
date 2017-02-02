using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ITweetRepository
    {
        IEnumerable<Tweet> GetNewerTweets();
        IEnumerable<Tweet> Mine(string userId);
        void AddTweet(string content, string userId);
        List<Tweet> GetTweetsByUsername(string username);
    }
}