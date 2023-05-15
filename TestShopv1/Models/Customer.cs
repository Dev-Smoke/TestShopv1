using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            ShoppingCards = new HashSet<ShoppingCard>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string EmailAddress { get; set; }
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
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        [InverseProperty(nameof(Order.Customer))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(ShoppingCard.Customer))]
        public virtual ICollection<ShoppingCard> ShoppingCards { get; set; }
    }
}
