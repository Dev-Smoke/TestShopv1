﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestShopv1.Models
{
    [MetadataType(typeof(Category))]
    public partial class Category
    {   
        //public decimal TaxRate10 { get; set; }
        //public decimal TaxRate20 { get; set; }
    }
}
