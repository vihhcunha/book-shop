using Book_Shop.Business.Interfaces.Notifications;
using Book_Shop.Business.Models;
using Book_Shop.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Book_Shop.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notificate(string property, string errorMessage)
        {
            _notificator.Handle(new Notification(errorMessage, property));
        }

        protected void Notificate(string errorMessage)
        {
            _notificator.Handle(new Notification(errorMessage));
        }

        protected void Notificate(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificate(error.PropertyName, error.ErrorMessage);
            }  
        }

        protected bool ExecuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity) where TValidation : AbstractValidator<TEntity> where TEntity : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notificate(validator);
            return false;
        }
    }
}
