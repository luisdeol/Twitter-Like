using System.Web.Mvc;
using WebApplication1.Core;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }
        // GET: Notifications
        public ActionResult Index()
        {
            var userProfileId = int.Parse(Session["sessionString"].ToString());
            var notifications = _unitOfWork.Notifications.GetNotReadNotifications(userProfileId);
            _unitOfWork.Notifications.MarkAsReadNotifications(notifications);
            _unitOfWork.Complete();
            return View(notifications);
        }
    }
}