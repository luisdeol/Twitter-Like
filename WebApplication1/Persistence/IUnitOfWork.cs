using WebApplication1.Repositories;

namespace WebApplication1.Persistence
{
    public interface IUnitOfWork
    {
        ITweetRepository Tweets { get; set; }
        IActivityRepository Activities { get; set; }
        IUserRepository Users { get; set; }
        void Complete();
    }
}