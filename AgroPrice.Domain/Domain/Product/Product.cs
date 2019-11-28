using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;
using AgroPrice.Domain.Domain.WholeSaleMarket;

namespace AgroPrice.Domain.Domain.Product
{
    /// <summary>
    /// Product Entity
    /// </summary>
    public class Product : BaseEntity
    {
        #region Basic Info
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the Origin
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// Gets or sets the SupplyDate
        /// </summary>
        public DateTime SupplyDate { get; set; }
        /// <summary>
        /// Gets or sets the ModificationDate
        /// </summary>
        public DateTime ModificationDate { get; set; }
        /// <summary>
        /// Gets or sets the PointOfSaleId
        /// </summary>
        public Guid PointOfSaleId { get; set; }
        /// <summary>
        /// Gets or sets the PointOfSale
        /// </summary>
        public virtual PointOfSale PointOfSale { get; set; }

        #endregion

        #region Related Info
        /// <summary>
        /// Gets product details.
        /// </summary>
        public virtual ICollection<ProductDetail> ProductDetails { get; protected set; } = new List<ProductDetail>();
        #endregion
    }
}
