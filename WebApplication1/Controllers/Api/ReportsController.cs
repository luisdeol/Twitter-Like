using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class ReportsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ReportsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Report(ActivityDto dto)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;

            if (_context.Activities.Any(a => a.TweetId == dto.TweetId &&
                                             a.UserId == userId &&
                                             a.ActivityType == ActivityTypes.TweetReport))
                return BadRequest();
            var activity = new Activity(userId, dto.TweetId, ActivityTypes.TweetReport);
            _context.Activities.Add(activity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;

            if (!_context.Activities.Any(a => a.TweetId == id &&
                                             a.UserId == userId &&
                                             a.ActivityType == ActivityTypes.TweetReport))
                return BadRequest();
            var activity = _context.Activities.Single(a=> a.TweetId == id &&
                                             a.UserId == userId &&
                                             a.ActivityType == ActivityTypes.TweetReport);
            _context.Activities.Remove(activity);
            _context.SaveChanges();
            return Ok();
        }
    }
}
