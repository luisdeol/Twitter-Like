using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ProfileViewModel
    {
        public string ProfileUser { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }
    }
}