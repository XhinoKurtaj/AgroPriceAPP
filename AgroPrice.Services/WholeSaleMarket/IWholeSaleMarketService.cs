using AgroPrice.Services.WholeSaleMarket.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroPrice.Services.WholeSaleMarket
{
    public interface IWholeSaleMarketService
    {
        Task<List<WholeSaleMarketModel>> GetAllWholeSaleMarkets();
    }
}
