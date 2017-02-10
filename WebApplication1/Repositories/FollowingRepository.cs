using System.Linq;
using WebApplication1.Models;


namespace WebApplication1.Repositories
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
    }
}