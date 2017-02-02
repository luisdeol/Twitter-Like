using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IApplicationDbContext _context;

        public ActivityRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public ILookup<int, Activity> GetLookupLikes(string userId)
        {
            return _context.Activities
                .Where(l => l.UserId == userId && 
                            l.ActivityType == ActivityTypes.TweetLike
                )
                .ToList()
                .ToLookup(l => l.TweetId);
        }

        public ILookup<int, Activity> GetLookupRetweets(string userId)
        {
            return _context.Activities
                .Where(r => r.UserId == userId &&
                            r.ActivityType == ActivityTypes.TweetRetweet)
                .ToList()
                .ToLookup(r => r.TweetId);
        }
        public ILookup<int, Activity> GetLookupReports(string userId)
        {
            return _context.Activities
                .Where(r => r.UserId == userId &&
                            r.ActivityType == ActivityTypes.TweetReport)
                .ToList()
                .ToLookup(r => r.TweetId);
        }
        public IEnumerable<Activity> GetMyActivities(string userId)
        {
            return _context.Activities
                .Include(l => l.Tweet)
                .Include(l => l.User)
                .Include(l => l.Tweet.User)
                .Where(l => l.UserId == userId);
        }

        public IEnumerable<Tweet> GetRetweetedTweets(string userId)
        {
            return _context.Activities
                .Where(a => a.UserId == userId && a.ActivityType == ActivityTypes.TweetRetweet)
                .Include(a => a.Tweet)
                .Select(a => a.Tweet)
                .Include(a => a.User);
        }

    }
}