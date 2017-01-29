using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IActivityRepository
    {
        ILookup<int, Activity> GetLookupRetweets(string userId);
        IEnumerable<Activity> GetMyActivities(string userId);
        ILookup<int, Activity> GetLookupLikes(string userId);
    }
}