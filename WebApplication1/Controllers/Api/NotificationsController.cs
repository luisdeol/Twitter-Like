using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }
        public IHttpActionResult GetNotifications()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var c = claims?.First();
            var userId = c?.Value;

            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);

            var notifications = _unitOfWork.Notifications.GetNotReadNotifications(userProfileId);

            return Ok(notifications);
        }
    }
}
