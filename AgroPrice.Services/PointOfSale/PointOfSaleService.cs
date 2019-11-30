using AgroPrice.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Services.PointOfSale.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AgroPrice.Functional;

namespace AgroPrice.Services.PointOfSale
{
    public class PointOfSaleService : IPointOfSaleService
    {
        private readonly IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> _pointOfSale;

        public PointOfSaleService(IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> pointOfSale)
        {
            _pointOfSale = pointOfSale;
        }

        public async Task<List<PointOfSaleModel>> GetAllPointOfSale()
        {
            return await _pointOfSale.TableNoTracking.ProjectTo<PointOfSaleModel>().ToListAsync();
        }

        public async Task<Result> CreateSellerWithPointOfSale(CreateSellerWithPointOfSaleModel model)
        {
            return Result.Ok();
        }

    }
}
