using System.Collections.Generic;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface ITweetRepository
    {
        IEnumerable<Tweet> GetNewerTweets(int userId);
        IEnumerable<Tweet> Mine(int userId);
        void AddTweet(string content, int userId);
        List<Tweet> GetTweetsByUserId(int username);
        void DeleteTweet(Tweet tweet);
        Tweet FindTweetById(int id, int userId);
    }
}