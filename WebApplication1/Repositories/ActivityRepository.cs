using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ILookup<int, Activity> GetLookupActivities(string userId)
        {
            return _context.Activities
                .Where(l => l.UserId == userId 
                && (l.ActivityType == ActivityTypes.TweetLike 
                    || l.ActivityType == ActivityTypes.TweetRetweet)
                )
                .ToList()
                .ToLookup(l => l.TweetId);
        }

        public IEnumerable<Activity> GetMyActivities(string userId)
        {
            return _context.Activities
                .Include(l => l.Tweet)
                .Include(l => l.User)
                .Include(l => l.Tweet.User)
                .Where(l => l.UserId == userId);
        }
    }
}