using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Persistence;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfilesController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: Profiles
        public ActionResult ShowProfile(string username)
        {
            string userId = User.Identity.GetUserId();

            var tweets = _unitOfWork.Tweets.GetTweetsByUsername(username);

            var retweetedTweets = _unitOfWork.Activities.GetRetweetedTweets(userId);

            tweets.AddRange(retweetedTweets);

            var viewModel = new ProfileViewModel
            {
                ProfileUser = username,
                Tweets = tweets
            };
            return View("Profile", viewModel);
        }
    }
}