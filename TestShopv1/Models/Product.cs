using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [MetadataType(typeof(_Product))]

    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPriceNetto { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public int? ProductNummer { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(ManufacturerId))]
        [InverseProperty("Products")]
        public virtual Manufacturer Manufacturer { get; set; }
        [InverseProperty(nameof(OrderLine.Product))]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
