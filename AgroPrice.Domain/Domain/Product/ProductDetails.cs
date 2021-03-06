﻿using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;

namespace AgroPrice.Domain.Domain.Product
{
    /// <summary>
    /// ProductDetails Entity
    /// </summary>
    public class ProductDetails : BaseEntity
    {
        #region Basic Info
        /// <summary>
        /// Gets or sets the CurrentPrice
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the CurrentQuantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the ModificationDate
        /// </summary>
        public DateTime ModificationDate { get; set; }
        /// <summary>
        /// Gets or sets the ProductId
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Gets or sets the Product
        /// </summary>
        public virtual Product Product { get; set; }

        #endregion

        #region Related Info

        // no properties yet

        #endregion
    }
}
