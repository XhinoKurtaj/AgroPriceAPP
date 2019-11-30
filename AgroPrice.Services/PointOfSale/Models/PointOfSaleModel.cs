using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Domain.Domain.User;

namespace AgroPrice.Services.PointOfSale.Models
{
    public class PointOfSaleModel : BaseModel
    {
        #region Basic Info
        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the Authentication User.
        /// </summary>
        public virtual User User { get; protected set; }

        /// <summary>
        /// Gets or sets the WholeSaleMarketId
        /// </summary>
        public Guid WholeSaleMarketId { get; set; }

        /// <summary>
        /// Gets the WholeSaleMarketId
        /// </summary>
        public virtual Domain.Domain.WholeSaleMarket.WholeSaleMarket WholeSaleMarket { get; protected set; }
        #endregion

       
    }
}
