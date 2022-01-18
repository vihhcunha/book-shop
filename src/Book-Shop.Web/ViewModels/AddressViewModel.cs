using Book_Shop.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book_Shop.Web.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field street it's mandatory.")]
        [StringLength(200, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "The field number it's mandatory.")]
        [StringLength(50, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 1)]
        public string Number { get; set; }
        public string? Complement { get; set; }

        [Required(ErrorMessage = "The field zip code it's mandatory.")]
        [StringLength(8, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 8)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "The field district it's mandatory.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 2)]
        public string District { get; set; }

        [Required(ErrorMessage = "The field city it's mandatory.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "The field state it's mandatory.")]
        [StringLength(50, ErrorMessage = "The field {0} must have between {2} and {1} caracters.", MinimumLength = 2)]
        public string State { get; set; }

        [HiddenInput]
        public Guid? ProviderId { get; set; }
    }
}
