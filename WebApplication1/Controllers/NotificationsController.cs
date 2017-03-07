using System.Linq;
using System.Web.Mvc;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Notifications
        public ActionResult Index()
        {
            var userProfileId = int.Parse(Session["sessionString"].ToString());
            var notifications = _context.Notifications
                .Where(n => n.UserId == userProfileId &&
                            !n.IsRead)
                .ToList();
            notifications.ForEach(n => n.Read());
            _context.SaveChanges();
            return View(notifications);
        }
    }
}