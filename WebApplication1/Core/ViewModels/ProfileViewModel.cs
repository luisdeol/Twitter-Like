using System.Collections.Generic;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.ViewModels
{
    public class ProfileViewModel
    {
        public string ProfileUsername { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }
        public bool IsFollowing { get; set; }
    }
}