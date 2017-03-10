using System.Collections.Generic;
using WebApplication1.Core.Models;

namespace WebApplication1.Core.Repositories
{
    public interface INotificationRepository
    {
        void CreateNotification(Notification notification);
        List<Notification> GetNotReadNotifications(int userId);
        void MarkAsReadNotifications(List<Notification> notifications);
    }
}