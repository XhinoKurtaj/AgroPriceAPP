using AgroPrice.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Services.PointOfSale.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AgroPrice.Functional;
using AgroPrice.Services.Product.Infrastructure;

namespace AgroPrice.Services.PointOfSale
{
    public class PointOfSaleService : IPointOfSaleService
    {
        private readonly IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> _pointOfSale;
        private readonly IRepository<Domain.Domain.Product.Product> _products;

        public PointOfSaleService(IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> pointOfSale, IRepository<Domain.Domain.Product.Product> products)
        {
            _pointOfSale = pointOfSale;
            _products = products;
        }

        public async Task<List<PointOfSaleModel>> GetAllPointOfSale()
        {
            return await _pointOfSale.TableNoTracking.ProjectTo<PointOfSaleModel>().ToListAsync();
        }

        public async Task<Result> CreateSellerWithPointOfSale(CreateSellerWithPointOfSaleModel model)
        {
            return Result.Ok();
        }

        public async Task<PointOfSaleDetailsModel> PointOfSaleDetails(Guid id)
        {
            var model = new PointOfSaleDetailsModel();

            var pointOfSaleDetails = await _pointOfSale.GetByIdAsync(id);
            var pointOfSaleModel = pointOfSaleDetails.ToModel<PointOfSaleModel>();
            model.PointOfSaleModel = pointOfSaleModel;
            var today = DateTime.Now.Date;
            var thisPointOfSaleProducts = _products.TableNoTracking.Where(x => x.PointOfSaleId == id);
            var todayPointOfSaleProducts =await thisPointOfSaleProducts.Where(x => x.RegisterDate.Date == today).ToListAsync();
            List<Models.Product> products = new List<Models.Product>();
            foreach (var productName in Products.ProductNames)
            {
                var newProduct = new Models.Product
                {
                    Name = productName
                };
                var thisProduct =todayPointOfSaleProducts.FirstOrDefault(x => x.Name == productName);
                if (thisProduct==null)
                {
                    products.Add(newProduct);
                }
                else
                {
                    newProduct.Id = thisProduct.Id;
                    newProduct.Origin = thisProduct.Origin;
                    newProduct.Price = thisProduct.Price;
                    newProduct.Quantity = thisProduct.Quantity;
                    newProduct.RegisterDate = thisProduct.RegisterDate;
                    products.Add(newProduct);
                }
            }

            model.Products = products;
            return model;

        }

    }
}
