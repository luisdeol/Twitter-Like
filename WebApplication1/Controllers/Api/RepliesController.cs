using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class RepliesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public RepliesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Reply(ReplyDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var activity = new Activity(userId, dto.TweetId, ActivityTypes.TweetReply, dto.ReplyContent);
            var notification = Notification.Replied(userId, dto.TweetId, dto.UserId);
            _context.Notifications.Add(notification);
            _context.Activities.Add(activity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteReply(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var reply = _context.Activities.SingleOrDefault(a => a.TweetId == id &&
                                                                 a.ActivityType == ActivityTypes.TweetReply &&
                                                                 a.UserId == userId);
            if (reply == null)
                return BadRequest();
            _context.Activities.Remove(reply);
            _context.SaveChanges();
            return Ok();
        }

    }
}
