using AgroPrice.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.PointOfSale
{
    public class PointOfSaleService : IPointOfSaleService
    {
        private readonly IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> _pointOfSale;

        public PointOfSaleService(IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> pointOfSale)
        {
            _pointOfSale = pointOfSale;
        }

    }
}
