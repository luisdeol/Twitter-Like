using System.Collections.Generic;
using System.Linq;
using WebApplication1.Core.Models;
using WebApplication1.Core.Repositories;

namespace WebApplication1.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserProfile> GetUsers(string userName)
        {
            return _context.UserProfiles.Where(u=> u.Username.Contains(userName)).ToList();
        }

        public UserProfile GetUserProfile(int id)
        {
            return _context.UserProfiles.SingleOrDefault(up => up.Id == id);
        }

        public int GetUserProfileId(string userId)
        {
            return _context.UserProfiles
                .Where(up => up.UserId == userId)
                .Select(up => up.Id)
                .First();
        }
    }
}