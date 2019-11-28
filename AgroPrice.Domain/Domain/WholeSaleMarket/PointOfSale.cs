using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;

namespace AgroPrice.Domain.Domain.WholeSaleMarket
{
    /// <summary>
    /// PointOfSale entity.
    /// </summary>
    public class PointOfSale : BaseEntity
    {
        #region Basic Info
        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the Authentication User.
        /// </summary>
        public virtual User.User User { get; protected set; }

        /// <summary>
        /// Gets or sets the WholeSaleMarketId
        /// </summary>
        public Guid WholeSaleMarketId { get; set; }

        /// <summary>
        /// Gets the WholeSaleMarketId
        /// </summary>
        public virtual WholeSaleMarket WholeSaleMarket { get; protected set; }
        #endregion

        #region Related Info

        /// <summary>
        /// Gets the list of products
        /// </summary>
        public virtual ICollection<Product.Product> Products { get; protected set; } = new List<Product.Product>();

        #endregion
    }
}
