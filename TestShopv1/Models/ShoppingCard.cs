using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Table("ShoppingCard")]
    public partial class ShoppingCard
    {
        [Key]
        public int Id { get; set; }
        public int ProductNummer { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal LinePrice { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("ShoppingCards")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(ManufacturerId))]
        [InverseProperty("ShoppingCards")]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
