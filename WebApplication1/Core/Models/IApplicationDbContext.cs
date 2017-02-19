using System.Data.Entity;

namespace WebApplication1.Core.Models
{
    public interface IApplicationDbContext
    {
        DbSet<Activity> Activities { get; set; }
        DbSet<Tweet> Tweets { get; set; }
    }
}