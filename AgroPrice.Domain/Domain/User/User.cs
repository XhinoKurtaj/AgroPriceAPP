using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Domain.Domain.User
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class User : IdentityUser
    {
        public Guid PointOfSaleId { get; set; }
        public virtual PointOfSale PointOfSale { get; protected set; }

        public Guid WholeSaleMarketId { get; set; }
        public virtual WholeSaleMarket.WholeSaleMarket WholeSaleMarket { get; protected set; }
    }
}
