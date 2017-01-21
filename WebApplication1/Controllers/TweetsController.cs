using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class TweetsController : Controller
    {
        private ApplicationDbContext _context;

        public TweetsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Tweets
        public ActionResult Index()
        {
            var tweets = _context.Tweets
                .Include(t => t.User)
                .OrderByDescending(t=>t.CreatedAt)
                .ToList();

            string userId = User.Identity.GetUserId();

            var likes = _context.Likes
                .Where(l => l.UserId == userId)
                .ToList()
                .ToLookup(l => l.TweetId);

            TweetsViewModel tvm = new TweetsViewModel
            {
                Tweets = tweets,
                IsAuthenticated = User.Identity.IsAuthenticated,
                TweetFormViewModel =  new TweetFormViewModel(),
                Likes = likes
            };
            return View(tvm);
        }


        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TweetsViewModel vm)
        {
            var userId = User.Identity.GetUserId();
            Tweet t = new Tweet(vm.TweetFormViewModel.Content, userId);
            _context.Tweets.Add(t);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var tweets = _context.Tweets.Include(t => t.User).Where(t => t.UserId == userId);

            return View(tweets);
        }

        [Authorize]
        public ActionResult MyActivities()
        {
            string userId = User.Identity.GetUserId();
            var likes = _context.Likes
                .Include(l=> l.Tweet)
                .Include(l=> l.User)
                .Include(l=> l.Tweet.User)
                .Where(l => l.UserId == userId);

            return View(likes);
        }
    }
}