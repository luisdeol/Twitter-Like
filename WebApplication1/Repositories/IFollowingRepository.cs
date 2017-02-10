using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IFollowingRepository
    {
        ILookup<string, Following> GetLookupFollowings(string userId);
    }
}