using Book_Shop.Business.Models;
using FluentValidation;

namespace Book_Shop.Business.Validations
{
    internal class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(2, 200).WithMessage("The field {PropertyName} must have {MinLength} and {MaxLength} caracters");

            RuleFor(c => c.District)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(2, 100).WithMessage("The field {PropertyName} must have {MinLength} and {MaxLength} caracters");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(8).WithMessage("The field {PropertyName} must have {MaxLength} caracters");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(2, 100).WithMessage("The field {PropertyName} must have {MinLength} and {MaxLength} caracters");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(2, 50).WithMessage("The field {PropertyName} must have {MinLength} and {MaxLength} caracters");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(1, 50).WithMessage("The field {PropertyName} must have {MinLength} and {MaxLength} caracters");
        }
    }
}
