using System.Collections.Generic;
using System.Linq;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface IActivityRepository
    {
        ILookup<int, Activity> GetLookupRetweets(string userId);
        IEnumerable<Activity> GetMyActivities(string userId);
        ILookup<int, Activity> GetLookupLikes(string userId);
        ILookup<int, Activity> GetLookupReports(string userId);
        IEnumerable<Tweet> GetRetweetedTweets(string userId);
    }
}