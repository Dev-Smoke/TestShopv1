using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Table("OrderLine")]
    public partial class OrderLine
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal? TotalPriceBrutto { get; set; }
        [Column(TypeName = "numeric(5, 2)")]
        public decimal? TaxRate { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderLines")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderLines")]
        public virtual Product Product { get; set; }
    }
}
