using Microsoft.AspNet.Identity;
using System.Linq;
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
            var context = new ApplicationDbContext();
            var profileId = context.UserProfiles.Where(u => u.UserId == userId).Select(up => up.Id).First();
            Session["sessionString"] = profileId;
            var profileCheck = Session["sessionString"];
            var tweets = _unitOfWork.Tweets.GetNewerTweets(profileId);
            return RedirectToAction("Index", "Tweets", tweets);
        }
    }
}