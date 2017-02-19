using System.Collections.Generic;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface IUserRepository
    {
        List<MyUserInfo> GetUsers(string userName);
    }
}