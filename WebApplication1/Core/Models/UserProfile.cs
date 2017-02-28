using System.Collections.Generic;

namespace WebApplication1.Core.Models
{
    public class UserProfile
    {

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }

        public int Id { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
    }
}