using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondThemes.BeyondAdmin.Models
{
    public class OrderDetails
    {
        public virtual CustomerOrder Order { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}