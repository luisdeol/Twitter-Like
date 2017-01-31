﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class RetweetsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public RetweetsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Retweet(ActivityDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;

            if (_context.Activities.Any(a => a.ActivityType == ActivityTypes.TweetRetweet && 
                                        a.UserId == userId &&
                                        a.TweetId == dto.TweetId))
                return BadRequest();

            var activity = new Activity(userId, dto.TweetId, ActivityTypes.TweetRetweet);
            _context.Activities.Add(activity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult CancelRetweet(int tweetId)
        {
            var identity = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;

            if (!_context.Activities.Any(a => a.TweetId == tweetId && 
                                            a.UserId == userId &&
                                            a.ActivityType == ActivityTypes.TweetRetweet))
                return BadRequest();

            var activity = _context.Activities.Single(a => a.TweetId == tweetId && a.UserId == userId);
            _context.Activities.Remove(activity);
            _context.SaveChanges();
            return Ok();
        }
    }
}