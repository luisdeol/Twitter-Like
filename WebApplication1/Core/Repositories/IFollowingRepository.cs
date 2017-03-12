using System.Linq;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface IFollowingRepository
    {
        ILookup<int, Following> GetLookupFollowings(int userId);
        bool GetIsFollowing(int userId, int visitUserId);
    }
}