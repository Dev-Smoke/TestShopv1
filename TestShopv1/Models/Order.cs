using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Column(TypeName = "money")]
        public decimal? TotalPriceBrutto { get; set; }
        [Column(TypeName = "date")]
        public DateTime? OrderedOn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PaidOn { get; set; }
        [StringLength(10)]
        public string RecipientSalutation { get; set; }
        [StringLength(25)]
        public string RecipientFirstname { get; set; }
        [StringLength(25)]
        public string RecipientLastname { get; set; }
        [StringLength(50)]
        public string RecipientStreet { get; set; }
        [StringLength(4)]
        public string RecipientZipCode { get; set; }
        [StringLength(25)]
        public string RecipientCity { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; }
        [InverseProperty(nameof(OrderLine.Order))]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
