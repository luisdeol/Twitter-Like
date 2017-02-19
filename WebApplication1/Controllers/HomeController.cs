using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebApplication1.Core;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var tweets = _unitOfWork.Tweets.GetNewerTweets(userId);
            return RedirectToAction("Index", "Tweets", tweets);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}