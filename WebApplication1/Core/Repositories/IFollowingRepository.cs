using System.Linq;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface IFollowingRepository
    {
        ILookup<string, Following> GetLookupFollowings(string userId);
        bool GetIsFollowing(string userId, string visitUsername);
    }
}