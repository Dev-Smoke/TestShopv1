using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            ShoppingCards = new HashSet<ShoppingCard>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Column(TypeName = "numeric(5, 2)")]
        public decimal? TaxRate { get; set; }

        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty(nameof(ShoppingCard.Category))]
        public virtual ICollection<ShoppingCard> ShoppingCards { get; set; }
    }
}
