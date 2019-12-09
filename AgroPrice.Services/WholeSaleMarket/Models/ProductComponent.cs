using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;

namespace AgroPrice.Services.WholeSaleMarket.Models
{
    public class ProductComponent : BaseModel
    {
        public string Name { get; set; }

        public double AvaragePrice { get; set; }

        public int AmountSum { get; set; }
    }
}
