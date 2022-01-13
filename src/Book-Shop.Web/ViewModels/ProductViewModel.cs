using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book_Shop.Web.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field name it's mandatory.")]
        [StringLength(200, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field description it's mandatory.")]
        [StringLength(1000, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Product image")]
        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "The field value it's mandatory.")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime RegistrationDate { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }

        public ProviderViewModel Provider { get; set; }
    }
}
