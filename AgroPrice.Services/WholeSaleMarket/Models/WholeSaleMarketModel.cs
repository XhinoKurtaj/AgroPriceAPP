using AgroPrice.Core;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.WholeSaleMarket.Models
{
    /// <summary>
    /// WholeSaleMarket model
    /// </summary>
    public class WholeSaleMarketModel : BaseModel
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
        public virtual ICollection<Domain.Domain.WholeSaleMarket.PointOfSale> PointOfSales { get; protected set; } = new List<Domain.Domain.WholeSaleMarket.PointOfSale>();


        #endregion
    }
}
