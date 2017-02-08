using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IUserRepository
    {
        List<MyUserInfo> GetUsers(string userName);
    }
}