using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Models;

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

        [HttpGet]
        public string Get()
        {
            return "Teste";
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var identities = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identities.Claims;
            Claim claim = claims?.First();
            var userId = claim?.Value;
            var tweet = _context.Tweets.Single(t => t.Id == id && t.UserId == userId);

            if (tweet == null)
                return BadRequest();

            _context.Tweets.Remove(tweet);
            _context.SaveChanges();

            return Ok();
        }
    }
}