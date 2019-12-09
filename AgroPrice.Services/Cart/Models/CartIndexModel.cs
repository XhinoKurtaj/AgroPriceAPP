using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.Product.Models;

namespace AgroPrice.Services.Cart.Models
{
    public class CartIndexModel
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public decimal TotalPrice { get; set; }

        public int TotalQuantity { get; set; }
    }
}
