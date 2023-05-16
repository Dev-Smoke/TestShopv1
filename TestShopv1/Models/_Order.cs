using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [NotMapped]
    [MetadataType(typeof(Order))]
    public partial class Order
    {
        [NotMapped]
        [DataType(DataType.Currency)]
        public decimal BruttoPreis { get; set; }
        [NotMapped]
        public string AlternativeLieferaddresse { get; set; }

    }
}
