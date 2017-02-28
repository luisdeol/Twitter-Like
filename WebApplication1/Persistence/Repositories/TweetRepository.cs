using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication1.Core.Models;
using WebApplication1.Core.Repositories;

namespace WebApplication1.Persistence.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly ApplicationDbContext _context;

        public TweetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tweet> GetNewerTweets(int userId)
        {
            var followees = _context.Followings.Where(f => f.FollowerId == userId).Select(f=> f.FolloweeId);
            return _context.Tweets
                .Where(t=> followees.Contains(t.UserId))
                .Include(t=> t.User)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }

        public IEnumerable<Tweet> Mine(int userId)
        {
            return _context.Tweets
                .Include(t => t.User)
                .Where(t => t.UserId == userId);
        }

        public void AddTweet(string content, int userId)
        {
            var tweet = new Tweet(content, userId);
            _context.Tweets.Add(tweet);
        }

        public List<Tweet> GetTweetsByUserId(int userId)
        {
            return _context.Tweets
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .ToList();
        }
    }
}