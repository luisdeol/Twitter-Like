using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class TweetsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public TweetsController()
        {
            _context = new ApplicationDbContext();
        }
        // DELETE api/<controller>/5

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var identities = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identities.Claims;
            Claim claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();
            var tweet = _context.Tweets.Single(t => t.Id == id && t.UserId == userProfileId);
            if (tweet == null)
                return BadRequest();

            _context.Tweets.Remove(tweet);
            _context.SaveChanges();

            return Ok();
        }
    }
}