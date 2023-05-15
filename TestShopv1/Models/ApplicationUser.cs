using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopv1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(10)]
        public string Salutation { get; set; }
        [Required]
        [StringLength(25)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(25)]
        public string Lastname { get; set; }
        [StringLength(50)]
        public string Street { get; set; }
        [StringLength(4)]
        public string ZipCode { get; set; }
        [StringLength(25)]
        public string City { get; set; }
    }
}
