using Book_Shop.Business.Interfaces.Notifications;

namespace Book_Shop.Business.Notifications
{
    public class Notificator : INotificator
    {
        private List<Notification> _notifications;
        public bool HasNotification => _notifications.Any();

        public Notificator()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }
    }
}
