using Book_Shop.Business.Notifications;

namespace Book_Shop.Business.Interfaces.Notifications
{
    public interface INotificator
    {
        bool HasNotification { get; }
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
