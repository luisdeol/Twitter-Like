using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();
            var following = new Following
            {
                FolloweeId = int.Parse(dto.FolloweeId),
                FollowerId = userProfileId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();
            var followeeId = int.Parse(id);
            var follow = _context.Followings.SingleOrDefault(f => f.FollowerId == userProfileId &&
                                                                  f.FolloweeId == followeeId);
            if (follow != null) _context.Followings.Remove(follow);
            _context.SaveChanges();
            return Ok();
        }
    }
}