using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    //[MetadataType(typeof(ShoppingCard))]
    public partial class ShoppingCard
    {
      
        //public int CustomerId { get; set; }
       
        //[ForeignKey(nameof(CustomerId))]
        //public Customer Customer { get; set; }
    }
}
