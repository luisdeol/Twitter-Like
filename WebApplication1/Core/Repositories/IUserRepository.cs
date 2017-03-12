using System.Collections.Generic;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface IUserRepository
    {
        List<UserProfile> GetUsers(string userName);
        UserProfile GetUserProfile(int id);
        int GetUserProfileId(string userId);
    }
}