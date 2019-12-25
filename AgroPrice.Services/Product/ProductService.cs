using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.Product;
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
        private readonly IRepository<ProductDetails> _productDetails;

        public ProductService(IRepository<Domain.Domain.Product.Product> product, IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> wholeSaleMarket, IRepository<ProductDetails> productDetails)
        {
            _product = product;
            _wholeSaleMarket = wholeSaleMarket;
            _productDetails = productDetails;
        }

        public async Task<Result> RegisterProduct(ProductModel model)
        {
            try
            {
                //add new product in database
                var product = new Domain.Domain.Product.Product
                {
                    Name = model.Name,
                    Origin = model.Origin,
                    RegisterDate = DateTime.Now,
                    PointOfSaleId = model.PointOfSaleId
                };
                await _product.InsertAsync(product);

                //add first product-details row for this product
                var productDetails = new ProductDetails()
                {
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ProductId = product.Id,
                    ModificationDate = product.RegisterDate
                };
                await _productDetails.InsertAsync(productDetails);
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
                    newProduct.Id = WholeSaleMarketsId;
                    //newProduct.AmountSum = thoseProducts.Sum(model=>model.Quantity);
                    //newProduct.AvaragePrice = thoseProducts.Average(model => model.Price);
                    list.Add(newProduct);
                }
            }

            model.WholeSaleMarketId = WholeSaleMarketsId;
            model.Products = list;
            return model;
        }

        public async Task<GetProductListByDateAndWholeSaleMarketModel> GetProductListByDateAndWholeSaleMarket(string WholeSaleMarketsID, DateTime startDate, DateTime endDate)
        {

            var model = new FindProductByWholeSaleMarketModel();
                var WholeSaleMarketsId = new Guid(WholeSaleMarketsID);
                var productListInTimeInterval = _product.TableNoTracking.Where(x =>
                    x.RegisterDate.Date > startDate && x.RegisterDate.Date < endDate);
                var productOfThisWholeSaleMarket = productListInTimeInterval
                    .Where(x => x.PointOfSale.WholeSaleMarketId == WholeSaleMarketsId);
                var list = new GetProductListByDateAndWholeSaleMarketModel();
                list.Product = new List<string>();
                list.DateTimeList = new List<List<DateTime>>();
                list.PriceList = new List<List<decimal>>();
                foreach (var productName in Products.ProductNames)
                {
                    list.Product.Add(productName);
                    var dateTime = new List<DateTime>();
                    var price = new List<decimal>();
                    var date = startDate;
                    while (date <= endDate)
                    {
                        var productInSpecificDate = productOfThisWholeSaleMarket.FirstOrDefault(x =>
                            x.Name == productName && x.RegisterDate.Date == date.Date);
                        ////var spePrice = productInSpecificDate == null ? 0 : productInSpecificDate.Price;
                        //price.Add(spePrice);
                        dateTime.Add(date);
                        date = date.AddDays(1);
                    }

                    list.DateTimeList.Add(dateTime);
                    list.PriceList.Add(price);
                }

                return list;
            
        }
    }
}
