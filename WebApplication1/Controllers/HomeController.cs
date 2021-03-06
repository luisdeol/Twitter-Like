﻿using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebApplication1.Core;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var profileId = _unitOfWork.Users.GetUserProfileId(userId);
            Session["sessionString"] = profileId;
            var tweets = _unitOfWork.Tweets.GetNewerTweets(profileId);
            return RedirectToAction("Index", "Tweets", tweets);
        }
    }
}