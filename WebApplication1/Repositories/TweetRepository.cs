using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private ApplicationDbContext _context;

        public TweetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tweet> GetNewerTweets()
        {
            
            return _context.Tweets
                .Include(t=> t.User)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }

        public IEnumerable<Tweet> Mine(string userId)
        {
            return _context.Tweets
                .Include(t => t.User)
                .Where(t => t.UserId == userId);
        }

        public void AddTweet(string content, string userId)
        {
            var tweet = new Tweet(content, userId);
            _context.Tweets.Add(tweet);
        }
    }
}