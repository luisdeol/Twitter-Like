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
        public ILookup<int, Activity> Likes { get; set; }
        public ILookup<int, Activity> Retweets { get; set; }
        public ILookup<int, Activity> Reports { get; set; }
        public ILookup<string, Following> Followings { get; set; }
    }
}