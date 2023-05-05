using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopv1.Models.ViewModel
{
    [NotMapped]
    public class CustomerVM
    {
        public int Id { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Passwortlänge beachten!")]
        public string Password { get; set; }

        [DisplayName("Retype Password")]
        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "JO, NA, DO FÖHTS MA AFOCH NUR AN WORTEN!")]
        [NotMapped]
        public string PasswordAgain { get; set; }

        [Required]
        [StringLength(10)]
        public string Salutation { get; set; }
        [Required]
        [StringLength(25)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(25)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(4)]
        public string ZipCode { get; set; }
        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        public bool AgreeToTerms { get; set; }

    }
}
