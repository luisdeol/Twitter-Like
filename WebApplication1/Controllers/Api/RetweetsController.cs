using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

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
            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();

            if (_context.Activities.Any(a => a.ActivityType == ActivityTypes.TweetRetweet && 
                                        a.UserId == userProfileId &&
                                        a.TweetId == dto.TweetId))
                return BadRequest();

            var activity = new Activity(userProfileId, dto.TweetId, ActivityTypes.TweetRetweet);
            _context.Activities.Add(activity);
            var notification = Notification.Retweeted(userProfileId, dto.TweetId, int.Parse(dto.UserId));
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult CancelRetweet(int id)
        {
            var identity = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();
            var activity = _context.Activities.SingleOrDefault(a => a.TweetId == id &&
                                                           a.UserId == userProfileId &&
                                                           a.ActivityType == ActivityTypes.TweetRetweet);
            if (activity == null)
                return BadRequest();
            _context.Activities.Remove(activity);
            _context.SaveChanges();
            return Ok();
        }
    }
}
