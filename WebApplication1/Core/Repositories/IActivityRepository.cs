using System.Collections.Generic;
using System.Linq;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface IActivityRepository
    {
        ILookup<int, Activity> GetLookupRetweets(int userId);
        IEnumerable<Activity> GetMyActivities(int userId);
        ILookup<int, Activity> GetLookupLikes(int userId);
        ILookup<int, Activity> GetLookupReports(int userId);
        IEnumerable<Tweet> GetRetweetedTweets(int userId);
        void AddActivity(Activity activity);
        Activity GetActivity(int tweetId, ActivityTypes type, int userId);
        void DeleteActivity(Activity activity);
    }
}