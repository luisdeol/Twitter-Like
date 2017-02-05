using System.Web.Http;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Api
{
    public class RepliesController : ApiController
    {
        private ApplicationDbContext _context;

        public RepliesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Reply(ReplyDto dto)
        {
            return Ok();
        }
    }
}
