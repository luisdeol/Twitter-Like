using WebApplication1.Core;
using WebApplication1.Core.Repositories;
using WebApplication1.Persistence.Repositories;

namespace WebApplication1.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITweetRepository Tweets { get; set; }
        public IActivityRepository Activities { get; set; }
        public IUserRepository Users { get; set; }
        public IFollowingRepository Followings { get; set; }
        public INotificationRepository Notifications { get; set; }
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Tweets = new TweetRepository(_context);
            Activities = new ActivityRepository(_context);
            Users = new UserRepository(_context);
            Followings = new FollowingRepository(_context);
            Notifications = new NotificationRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}