using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    
    public partial class _Category
    {
        [Required(ErrorMessage = "Geben sie einen gültigen Namen ein")]
        [StringLength(25, MinimumLength = 4)]
        public string Name { get; set; }

        [DisplayName("Steuersatz")]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? TaxRate { get; set; }

        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
