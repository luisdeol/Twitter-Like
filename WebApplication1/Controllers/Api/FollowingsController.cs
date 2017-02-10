using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Dtos;
using WebApplication1.Models;

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

            var following = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
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

            var follow = _context.Followings.SingleOrDefault(f => f.FollowerId == userId &&
                                                                  f.FolloweeId == id);
            if (follow != null) _context.Followings.Remove(follow);
            _context.SaveChanges();
            return Ok();
        }
    }
}