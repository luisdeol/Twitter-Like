using System.Web.Mvc;
using WebApplication1.Models;
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
            var tweets = _unitOfWork.Tweets.GetNewerTweets();
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