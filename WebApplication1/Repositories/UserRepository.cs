using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<MyUserInfo> GetUsers(string userName)
        {
            return _context.MyUserInfos.Where(u=> u.Username==userName).ToList();
        }
    }
}