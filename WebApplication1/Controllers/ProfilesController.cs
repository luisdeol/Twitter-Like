using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProfilesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Profiles
        public ActionResult ShowProfile(string username)
        {
            string userId = User.Identity.GetUserId();

            var tweets = _context.Tweets
                .Where(t => t.User.UserName == username)
                .Include(t=> t.User)
                .ToList();

            var likedTweets = _context.Activities
                .Where(l => l.UserId == userId)
                .Include(l => l.Tweet)
                .Select(l=> l.Tweet)
                .Include(l => l.User);

            tweets.AddRange(likedTweets);

            var viewModel = new ProfileViewModel
            {
                ProfileUser = username,
                Tweets = tweets
            };
            return View("Profile", viewModel);
        }
    }
}