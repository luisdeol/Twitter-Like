using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class TweetsViewModel
    {
        public IEnumerable<Tweet> Tweets { get; set; }
        public bool IsAuthenticated { get; set; }
        public TweetFormViewModel TweetFormViewModel { get; set; }
        public ILookup<int, Like> Likes { get; set; }
    }
}