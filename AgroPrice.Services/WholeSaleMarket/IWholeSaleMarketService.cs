using AgroPrice.Services.WholeSaleMarket.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Functional;

namespace AgroPrice.Services.WholeSaleMarket
{
    public interface IWholeSaleMarketService
    {
        Task<List<WholeSaleMarketModel>> GetAllWholeSaleMarkets();
        
        //Delete whole sale market
        Task<Result> DeleteWholeSaleMarket(Guid id);
    }
}
