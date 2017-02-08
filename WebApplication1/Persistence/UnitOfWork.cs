using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITweetRepository Tweets { get; set; }
        public IActivityRepository Activities { get; set; }
        public IUserRepository Users { get; set; }

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Tweets = new TweetRepository(_context);
            Activities = new ActivityRepository(_context);
            Users = new UserRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}