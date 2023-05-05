using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    
    public partial class _Product
    {
        

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        
        public decimal UnitPriceNetto { get; set; } 
        public string ImagePath { get; set; }

        [DisplayName("Beschreibung")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int ManufacturerId { get; set; }

        [DisplayName("Kategorie")]
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }

        [DisplayName("Hersteller")]
        [ForeignKey(nameof(ManufacturerId))]
        [InverseProperty("Products")]
        public virtual Manufacturer Manufacturer { get; set; }

        [InverseProperty(nameof(OrderLine.Product))]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
