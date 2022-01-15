using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Book_Shop.Web.Extensions
{
    public class MoneyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                var money = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Money have a wrong format.");
            }
            return ValidationResult.Success;
        }
    }

    public class MoneyAttributeAdapter : AttributeAdapterBase<MoneyAttribute>
    {
        public MoneyAttributeAdapter(MoneyAttribute attribute, IStringLocalizer? stringLocalizer) : base(attribute, stringLocalizer)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-money", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "Money have a wrong format.";
        }
    }

    public class MoneyValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _provider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter? GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer? stringLocalizer)
        {
            if (attribute is MoneyAttribute moneyAttribute)
            {
                return new MoneyAttributeAdapter(moneyAttribute, stringLocalizer);
            }

            return _provider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
