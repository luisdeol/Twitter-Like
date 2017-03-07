using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetNotifications()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var c = claims?.First();
            var userId = c?.Value;

            var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();

            var notifications = _context.Notifications
                .Where(n => n.UserId == userProfileId &&
                            !n.IsRead)
                .ToList();

            return Ok(notifications);
        }

        //[HttpPost]
        //public IHttpActionResult MarkAsRead()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var claims = identity.Claims;
        //    var c = claims?.First();
        //    var userId = c?.Value;

        //    var userProfileId = _context.UserProfiles.Where(up => up.UserId == userId).Select(u => u.Id).First();

        //    var notifications = _context.Notifications
        //        .Where(n => n.UserId == userProfileId &&
        //                    !n.IsRead)
        //        .ToList();
        //    notifications.ForEach(n => n.Read());
        //    _context.SaveChanges();
        //    return Ok();
        //}
    }
}
