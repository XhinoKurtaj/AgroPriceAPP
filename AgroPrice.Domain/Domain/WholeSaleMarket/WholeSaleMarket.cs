using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;
using AgroPrice.Domain.Domain.WholeSaleMarket;

namespace AgroPrice.Domain.Domain.WholeSaleMarket
{
    /// <summary>
    /// WholeSaleMarket entity
    /// </summary>
    public class WholeSaleMarket : BaseEntity
    {
        #region
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///  Gets or sets the ImageUrl
        /// </summary>
        public string ImageUrl { get; set; }

        #endregion

        #region Related Info

        /// <summary>
        /// Gets the pointOfSales
        /// </summary>
        public virtual ICollection<PointOfSale> PointOfSales { get; protected set; } = new List<PointOfSale>();

        
        #endregion

    }
}
