using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Functional;
using AgroPrice.Services.Product.Models;

namespace AgroPrice.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Domain.Domain.Product.Product> _product;

        public ProductService(IRepository<Domain.Domain.Product.Product> product)
        {
            _product = product;
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
    }
}
