using Book_Shop.Business.Interfaces.Notifications;

namespace Book_Shop.Business.Notifications
{
    public class Notification
    {
        public string Message { get; }

        public Notification(string message)
        {
            Message = message;
        }
    }
}
