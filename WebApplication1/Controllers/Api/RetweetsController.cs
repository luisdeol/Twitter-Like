using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class RetweetsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RetweetsController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult Retweet(ActivityDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var retweet = _unitOfWork.Activities.GetActivity(dto.TweetId, ActivityTypes.TweetRetweet, userProfileId);
            if (retweet != null)
                return BadRequest();
            var activity = new Activity(userProfileId, dto.TweetId, ActivityTypes.TweetRetweet);
            _unitOfWork.Activities.AddActivity(activity);
            var notification = Notification.Retweeted(userProfileId, dto.TweetId, int.Parse(dto.UserId));
            _unitOfWork.Notifications.CreateNotification(notification);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult CancelRetweet(int id)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var activity = _unitOfWork.Activities.GetActivity(id, ActivityTypes.TweetRetweet, userProfileId);
            if (activity == null)
                return BadRequest();
            _unitOfWork.Activities.DeleteActivity(activity);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
