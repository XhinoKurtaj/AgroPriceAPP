using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;

namespace AgroPrice.Services.WholeSaleMarket.Models
{
    public class GetProductListByDateAndWholeSaleMarketModel
    {
        public List<string> Product { get; set; }
        public List<List<DateTime>> DateTimeList { get; set; }
        public List<List<decimal>> PriceList { get; set; }
    }

}
