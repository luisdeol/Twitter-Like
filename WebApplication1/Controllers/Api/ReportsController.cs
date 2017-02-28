using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

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
            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();
            if (_context.Activities.Any(a => a.TweetId == dto.TweetId &&
                                             a.UserId == userProfileId &&
                                             a.ActivityType == ActivityTypes.TweetReport))
                return BadRequest();
            var activity = new Activity(userProfileId, dto.TweetId, ActivityTypes.TweetReport);
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
            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();
            var activity = _context.Activities.SingleOrDefault(a => a.TweetId == id &&
                                                                    a.UserId == userProfileId &&
                                                                    a.ActivityType == ActivityTypes.TweetReport);
            if (activity == null)
                return BadRequest();
            _context.Activities.Remove(activity);
            _context.SaveChanges();
            return Ok();
        }
    }
}
