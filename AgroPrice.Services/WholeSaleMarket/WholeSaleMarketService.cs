using AgroPrice.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket.Models;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Services.WholeSaleMarket
{
    public class WholeSaleMarketService : IWholeSaleMarketService
    {
        private readonly IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> _wholeSaleMarket;

        public WholeSaleMarketService(IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> wholeSaleMarket)
        {
            _wholeSaleMarket = wholeSaleMarket;
        }

        public async Task<List<WholeSaleMarketModel>> GetAllWholeSaleMarkets()
        {
            return await _wholeSaleMarket.TableNoTracking.ProjectTo<WholeSaleMarketModel>().ToListAsync();
        }
        
        
    }
}
