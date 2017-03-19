using System.Web.Mvc;
using WebApplication1.Core;
using WebApplication1.Core.Models;
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
        public ActionResult ShowProfile(int visitUserId)
        {
            var userId = int.Parse(Session["sessionString"].ToString());
            var visitProfile = _unitOfWork.Users.GetUserProfile(visitUserId);

            var tweets = _unitOfWork.Tweets.GetTweetsByUserId(visitProfile.Id);
            
            var retweetedTweets = _unitOfWork.Activities.GetRetweetedTweets(userId);

            tweets?.AddRange(retweetedTweets);

            var isFollowing = _unitOfWork.Followings.GetIsFollowing(userId, visitUserId);

            var viewModel = new ProfileViewModel
            {
                ProfileUsername = visitProfile.Username,
                UserId = visitProfile.Id,
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

        public ActionResult Edit()
        {
            var userId = int.Parse(Session["sessionString"].ToString());
            var profile = _unitOfWork.Users.GetUserProfile(userId);
            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit(UserProfile profile)
        {
            _unitOfWork.Users.EditUserProfile(profile);
            _unitOfWork.Complete();
            return RedirectToAction("ShowProfile", new { visitUserId = profile.Id}); 
        }
    }
}