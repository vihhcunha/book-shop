using Book_Shop.Business.Models;
using FluentValidation;

namespace Book_Shop.Business.Validations
{
    internal class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(2, 200).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled")
                .Length(2, 1000).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("The field {PropertyName} must be bigger than {ComparisonValue}");
        }
    }
}
