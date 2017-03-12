using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class LikesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public LikesController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult Like(ActivityDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            var c = claims?.First();
            var userId = c?.Value;

            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var existingLike = _unitOfWork.Activities.GetActivity(dto.TweetId, ActivityTypes.TweetLike, userProfileId);

            if (existingLike != null)
                return BadRequest();

            var like = new Activity(userProfileId, dto.TweetId, ActivityTypes.TweetLike);

            _unitOfWork.Activities.AddActivity(like);
            var notification = Notification.Liked(userProfileId, dto.TweetId, int.Parse(dto.UserId));
            _unitOfWork.Notifications.CreateNotification(notification);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Dislike(int id)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;

            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);

            var like = _unitOfWork.Activities.GetActivity(id, ActivityTypes.TweetLike, userProfileId);

            if (like == null)
                return BadRequest();

            _unitOfWork.Activities.DeleteActivity(like);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
