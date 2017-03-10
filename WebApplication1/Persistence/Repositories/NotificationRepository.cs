using System.Collections.Generic;
using System.Linq;
using WebApplication1.Core.Models;
using WebApplication1.Core.Repositories;

namespace WebApplication1.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        public List<Notification> GetNotReadNotifications(int userId)
        {
            return _context.Notifications
                .Where(n => n.UserId == userId &&
                            !n.IsRead)
                .ToList();
        }

        public void MarkAsReadNotifications(List<Notification> notifications)
        {
            notifications.ForEach(n=> n.Read());
        }
    }
}