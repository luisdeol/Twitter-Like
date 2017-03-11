using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class TweetsController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;

        public TweetsController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }
        // DELETE api/<controller>/5

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var identities = (ClaimsIdentity) User.Identity;
            IEnumerable<Claim> claims = identities.Claims;
            Claim claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var tweet = _unitOfWork.Tweets.FindTweetById(id, userProfileId);
            if (tweet == null)
                return BadRequest();
            _unitOfWork.Tweets.DeleteTweet(tweet);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}