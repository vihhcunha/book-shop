using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Book_Shop.Web.ViewModels
{
    public class ProviderViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field name it's mandatory.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field document it's mandatory.")]
        [StringLength(14, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 11)]
        public string Document { get; set; }

        [DisplayName("Kind")]
        public int ProviderKind { get; set; }

        public AddressViewModel? Address { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }

        public IEnumerable<ProductViewModel>? Products { get; set; }
    }
}
