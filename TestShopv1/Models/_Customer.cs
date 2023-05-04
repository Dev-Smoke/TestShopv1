using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopv1.Models
{
    [NotMapped]
    public partial class Customer
    {
        [NotMapped]
        public Guid Guid { get; set; }
        [NotMapped]
        public Guid GuidSalt { get; set; }



    }
}
