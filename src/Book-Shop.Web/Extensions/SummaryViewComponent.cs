using Book_Shop.Business.Interfaces.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Web.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificator _notificator;

        public SummaryViewComponent(INotificator notificator)
        {
            _notificator = notificator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notificator.GetNotifications());
            notifications.ForEach(notification => ViewData.ModelState.AddModelError(string.Empty, notification.Message));
            return View();
        }
    }
}
