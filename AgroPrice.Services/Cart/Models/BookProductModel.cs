using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;

namespace AgroPrice.Services.Cart.Models
{
    public class BookProductModel:BaseModel
    {
        public decimal SaleQuantity { get; set; }

        public decimal BookQuantity { get; set; }
    }
}
