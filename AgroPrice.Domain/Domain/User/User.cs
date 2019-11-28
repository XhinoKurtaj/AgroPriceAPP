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
        public virtual PointOfSale PointOfSale { get; protected set; }
    }
}
