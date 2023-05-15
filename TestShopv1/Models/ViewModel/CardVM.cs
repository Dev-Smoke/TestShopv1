using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestShopv1.Models.ViewModel
{
    public class CardVM
    {
        public List<ShoppingCard> CardList { get; set; }
        public OrderLine OrderLine { get; set; }
        public decimal? OrderTotal { get; set; }

    }
}
