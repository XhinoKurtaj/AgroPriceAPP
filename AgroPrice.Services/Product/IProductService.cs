using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Functional;
using AgroPrice.Services.Product.Models;
using AgroPrice.Services.WholeSaleMarket.Models;

namespace AgroPrice.Services.Product
{
    public interface IProductService
    {
        Task<Result> RegisterProduct(ProductModel model);
        Task<FindProductByWholeSaleMarketModel> FindProductByWholeSaleMarket(string WholeSaleMarketsID);
    }
}
