using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;

namespace AgroPrice.Services.Product.Models
{
    /// <summary>
    /// Product Model
    /// </summary>
    public class ProductModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the Price
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Gets or sets the Origin
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// Gets or sets the SupplyDate
        /// </summary>
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// Gets or sets the PointOfSaleId
        /// </summary>
        public Guid PointOfSaleId { get; set; }
        /// <summary>
        /// Gets or sets the PointOfSale
        /// </summary>
        public virtual Domain.Domain.WholeSaleMarket.PointOfSale PointOfSale { get; set; }
    }
}
