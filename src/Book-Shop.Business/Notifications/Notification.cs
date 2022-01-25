namespace Book_Shop.Business.Notifications
{
    public class Notification
    {
        public string Message { get; }
        public string PropertyName { get; }

        public Notification(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;
        }

        public Notification(string message)
        {
            Message = message;
        }
    }
}
