using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class LikesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public LikesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Like(ActivityDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim c = claims?.First();
            string userId = c?.Value;

            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u=> u.Id).First();

            if (_context.Activities.Any(n => n.TweetId == dto.TweetId && 
                                        n.UserId == userProfileId &&
                                        n.ActivityType == ActivityTypes.TweetLike))
                return BadRequest();

            var like = new Activity(userProfileId, dto.TweetId, ActivityTypes.TweetLike);

            _context.Activities.Add(like);
            var notification = Notification.Liked(userProfileId, dto.TweetId, int.Parse(dto.UserId));
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Dislike(int id)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            Claim claim = claims?.First();
            var userId = claim?.Value;

            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();

            var like = _context.Activities
                .Single(l => l.UserId == userProfileId && 
                             l.TweetId == id && 
                             l.ActivityType == ActivityTypes.TweetLike);

            if (like == null)
                return BadRequest();

            _context.Activities.Remove(like);
            _context.SaveChanges();

            return Ok();
        }
    }
}
