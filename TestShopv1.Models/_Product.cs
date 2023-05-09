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
    }
}
