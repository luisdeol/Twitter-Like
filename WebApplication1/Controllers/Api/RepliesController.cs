using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class RepliesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepliesController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult Reply(ReplyDto dto)
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var activity = new Activity(userProfileId, dto.TweetId, ActivityTypes.TweetReply, dto.ReplyContent);
            var notification = Notification.Replied(userProfileId, dto.TweetId, int.Parse(dto.UserId));
            _unitOfWork.Notifications.CreateNotification(notification);
            _unitOfWork.Activities.AddActivity(activity);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteReply(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var reply = _unitOfWork.Activities.GetActivity(id, ActivityTypes.TweetReply, userProfileId);
            if (reply == null)
            {
                return BadRequest();
            }
            _unitOfWork.Activities.DeleteActivity(reply);
            _unitOfWork.Complete();
            return Ok();
        }

    }
}
