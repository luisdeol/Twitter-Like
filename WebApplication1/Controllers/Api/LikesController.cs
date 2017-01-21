using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class LikesController : ApiController
    {
        private ApplicationDbContext _context;

        public LikesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Like(LikeDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim c = claims?.First();
            string userId = c?.Value;

            if (_context.Likes.Any(n => n.TweetId == dto.TweetId && n.UserId == userId))
                return BadRequest();

            var like = new Like(userId, dto.TweetId);

            _context.Likes.Add(like);
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

            var like = _context.Likes.Single(l => l.UserId == userId && l.TweetId == id);

            if (like == null)
                return BadRequest();

            _context.Likes.Remove(like);
            _context.SaveChanges();

            return Ok();
        }
    }
}
