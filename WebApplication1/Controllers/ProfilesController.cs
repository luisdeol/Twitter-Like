using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProfilesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Profiles
        public ActionResult ShowProfile(string id)
        {
            string userId = User.Identity.GetUserId();
            var tweets = _context.Tweets
                .Include(t=> t.User)
                .Where(t => t.UserId == id);

            var viewModel = new ProfileViewModel
            {
                ProfileUser = tweets.First().User,
                Tweets = tweets
            };
            return View("Profile", viewModel);
        }
    }
}