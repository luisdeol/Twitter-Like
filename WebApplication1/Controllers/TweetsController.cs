﻿using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Persistence;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class TweetsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TweetsController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }
        // GET: Tweets
        public ActionResult Index()
        {
            var tweets = _unitOfWork.Tweets.GetNewerTweets();

            string userId = User.Identity.GetUserId();

            var likes = _unitOfWork.Activities.GetLookupLikes(userId);
            var retweets = _unitOfWork.Activities.GetLookupRetweets(userId);
            var reports = _unitOfWork.Activities.GetLookupReports(userId);

            TweetsViewModel tvm = new TweetsViewModel
            {
                Tweets = tweets,
                IsAuthenticated = User.Identity.IsAuthenticated,
                TweetFormViewModel =  new TweetFormViewModel(),
                Likes = likes,
                Retweets =  retweets,
                Reports = reports
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
            _unitOfWork.Tweets.AddTweet(vm.TweetFormViewModel.Content, userId);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var tweets = _unitOfWork.Tweets.Mine(userId);

            return View(tweets);
        }

        [Authorize]
        public ActionResult MyActivities()
        {
            string userId = User.Identity.GetUserId();
            var likes = _unitOfWork.Activities.GetMyActivities(userId);

            return View(likes);
        }
    }
}