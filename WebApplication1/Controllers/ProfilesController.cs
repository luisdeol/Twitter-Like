using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Core;
using WebApplication1.Core.ViewModels;
using WebApplication1.Persistence;

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
        public ActionResult ShowProfile(string visitUserId)
        {
            var userId = User.Identity.GetUserId();

            var tweets = _unitOfWork.Tweets.GetTweetsByUsername(visitUserId);
            var userName = tweets?.First().User.Name;
            var retweetedTweets = _unitOfWork.Activities.GetRetweetedTweets(visitUserId);

            tweets?.AddRange(retweetedTweets);

            var isFollowing = _unitOfWork.Followings.GetIsFollowing(userId, userName);
            var viewModel = new ProfileViewModel
            {
                ProfileUsername = userName,
                UserId = visitUserId,
                Tweets = tweets,
                IsFollowing = isFollowing
            };
            return View("Profile", viewModel);
        }

        [HttpPost]
        public ActionResult Search(string searchQuery=null)
        {
            var users = _unitOfWork.Users.GetUsers(searchQuery);
            return View(users);
        }
    }
}