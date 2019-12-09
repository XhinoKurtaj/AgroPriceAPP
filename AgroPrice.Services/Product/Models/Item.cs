using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.Product.Models
{
    public class Item
    {
        public ProductInCartModel Product { get; set; }

        public int Quantity { get; set;}
    }
}
