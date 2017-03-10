using WebApplication1.Core.Repositories;

namespace WebApplication1.Core
{
    public interface IUnitOfWork
    {
        ITweetRepository Tweets { get; set; }
        IActivityRepository Activities { get; set; }
        IUserRepository Users { get; set; }
        IFollowingRepository Followings { get; set; }
        INotificationRepository Notifications { get; set; }
        void Complete();
    }
}