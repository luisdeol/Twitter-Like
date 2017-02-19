using System.Linq;
using WebApplication1.Core.Models;
using WebApplication1.Core.Repositories;

namespace WebApplication1.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ILookup<string, Following> GetLookupFollowings(string userId)
        {
            return _context.Followings.Where(f => f.FollowerId == userId)
                .ToList()
                .ToLookup(f => f.FolloweeId);
        }

        public bool GetIsFollowing(string userId, string visitUsername)
        {
            var c = _context.Followings
                .Any(f => f.Followee.Name == visitUsername &&
                                                f.FollowerId == userId);
            return c;
        }
    }
}