using Book_Shop.Business.Models;
using Book_Shop.Business.Validations.Documents;
using FluentValidation;

namespace Book_Shop.Business.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(_ => _.Name)
                .NotEmpty().WithMessage("The field {PropertyName} must be filled.")
                .Length(2, 100).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters");

            When(_ => _.ProviderKind == ProviderKind.IndividualEntity, () =>
            {
                RuleFor(_ => _.Document.Length).Equal(CpfValidations.CpfSize).WithMessage("The document field must have {ComparisonValue} caracters");
                RuleFor(_ => CpfValidations.Validate(_.Document)).Equal(true).WithMessage("The document it's invalid");
            });

            When(_ => _.ProviderKind == ProviderKind.LegalEntity, () =>
            {
                RuleFor(_ => _.Document.Length).Equal(CnpjValidations.CnpjSize).WithMessage("The document field must have {ComparisonValue} caracters");
                RuleFor(_ => CnpjValidations.Validate(_.Document)).Equal(true).WithMessage("The document it's invalid");
            });
        }
    }
}
