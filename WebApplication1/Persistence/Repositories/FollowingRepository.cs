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

        public ILookup<int, Following> GetLookupFollowings(int userId)
        {
            return _context.Followings.Where(f => f.FollowerId == userId)
                .ToList()
                .ToLookup(f => f.FolloweeId);
        }

        public bool GetIsFollowing(int userId, int visitUserId)
        {
            var isFollowing = _context.Followings
                .Any(f => f.FolloweeId == visitUserId &&
                                                f.FollowerId == userId);
            return isFollowing;
        }
    }
}