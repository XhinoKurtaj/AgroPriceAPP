﻿using AgroPrice.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.PointOfSale.Models
{
    public class UpdateSellerWithPointOfSaleModel : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }

        public string PointOfSaleDescription { get; set; }

        public string WholeSaleMarketId { get; set; }

        public Guid UserId { get; set; }
    }
}
