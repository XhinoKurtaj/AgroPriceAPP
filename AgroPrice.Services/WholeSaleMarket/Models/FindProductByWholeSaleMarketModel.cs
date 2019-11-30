using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.WholeSaleMarket.Models
{
    public class FindProductByWholeSaleMarketModel
    {
        public Guid WholeSaleMarketId { get; set; }

        public List<ProductComponent> Products { get; set; } = new List<ProductComponent>();

    }
}
