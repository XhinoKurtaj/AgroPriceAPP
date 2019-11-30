using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Functional;
using AgroPrice.Services.Product.Infrastructure;
using AgroPrice.Services.Product.Models;
using AgroPrice.Services.WholeSaleMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Domain.Domain.Product.Product> _product;
        private readonly IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> _wholeSaleMarket;

        public ProductService(IRepository<Domain.Domain.Product.Product> product, IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> wholeSaleMarket)
        {
            _product = product;
            _wholeSaleMarket = wholeSaleMarket;
        }

        public async Task<Result> RegisterProduct(ProductModel model)
        {
            try
            {
                var entity = new Domain.Domain.Product.Product
                {
                    Name = model.Name,
                    Origin = model.Origin,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    RegisterDate = DateTime.Now,
                    PointOfSaleId = model.PointOfSaleId
                };
                await _product.InsertAsync(entity);
            }
            catch
            {
                return Result.Fail("","");
            }
            return Result.Ok();
        }

        public async Task<FindProductByWholeSaleMarketModel> FindProductByWholeSaleMarket(string WholeSaleMarketsID)
        {
            var model = new FindProductByWholeSaleMarketModel();
            var WholeSaleMarketsId = new Guid(WholeSaleMarketsID);
            var todayProducts = _product.TableNoTracking.Where(x => x.RegisterDate.Date == DateTime.Now.Date);
            var productOfThisWholeSaleMarket = todayProducts
                .Where(x => x.PointOfSale.WholeSaleMarketId == WholeSaleMarketsId);
            var list = new List<ProductComponent>();
            foreach (var productName in Products.ProductNames)
            {
                var newProduct = new ProductComponent()
                {
                    Name = productName
                };
                var thoseProducts = productOfThisWholeSaleMarket.Where(x => x.Name == productName);
                if (!thoseProducts.Any())
                {
                    list.Add(newProduct);
                }
                else
                {
                    newProduct.AmountSum = thoseProducts.Sum(model=>model.Quantity);
                    newProduct.AvaragePrice = thoseProducts.Average(model => model.Price);
                    list.Add(newProduct);
                }
            }

            model.WholeSaleMarketId = WholeSaleMarketsId;
            model.Products = list;
            return model;
        }
    }
}
