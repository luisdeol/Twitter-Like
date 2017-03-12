using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebApplication1.Core;
using WebApplication1.Core.Dtos;
using WebApplication1.Core.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers.Api
{
    public class ReportsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportsController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult Report(ActivityDto dto)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var report = _unitOfWork.Activities.GetActivity(dto.TweetId, ActivityTypes.TweetReport, userProfileId);
            if (report != null)
                return BadRequest();
            var activity = new Activity(userProfileId, dto.TweetId, ActivityTypes.TweetReport);
           _unitOfWork.Activities.AddActivity(activity);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var claim = claims?.First();
            var userId = claim?.Value;
            var userProfileId = _unitOfWork.Users.GetUserProfileId(userId);
            var report = _unitOfWork.Activities.GetActivity(id, ActivityTypes.TweetReport, userProfileId);

            if (report == null)
                return BadRequest();
            _unitOfWork.Activities.DeleteActivity(report);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
