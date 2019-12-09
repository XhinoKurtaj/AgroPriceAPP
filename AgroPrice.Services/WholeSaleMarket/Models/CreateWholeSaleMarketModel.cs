using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;

namespace AgroPrice.Services.WholeSaleMarket.Models
{
    public class CreateWholeSaleMarketModel : BaseModel
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
    }

}
