using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Table("Manufacturer")]
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
            ShoppingCards = new HashSet<ShoppingCard>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [InverseProperty(nameof(Product.Manufacturer))]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty(nameof(ShoppingCard.Manufacturer))]
        public virtual ICollection<ShoppingCard> ShoppingCards { get; set; }
    }
}
