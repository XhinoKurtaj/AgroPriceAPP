using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Services.Product;
using AgroPrice.Services.Product.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AgroPrice.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IRepository<Product> _product;

        public ProductController(IProductService productService, IRepository<Product> product)
        {
            _productService = productService;
            _product = product;
        }

        [HttpGet]
        public IActionResult RegisterProduct(string productName , Guid id)
        {
            var entity  = new Product
            {
                PointOfSaleId = id,
                Name = productName
            };
            var model = entity.ToModel<ProductModel>(); 
            return PartialView("_Partials/RegisterProduct",model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Partials/RegisterProduct", model);
            }

            var result = await _productService.RegisterProduct(model);
            if (!result.Success)
            {
                return PartialView("_Partials/RegisterProduct", model);
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> EditQuantity(Guid id)
        {
            var entity = await _product.GetByIdAsync(id);
            var model = new EditQuantityModel
            {
                Id = id,
                Quantity = entity.Quantity
            };
            return PartialView("_Partials/_EditQuantity", model);
        }
        [HttpPost]
        public async Task<IActionResult> EditQuantityAsync(EditQuantityModel model)
        {
            try
            {
                var entity = await _product.GetByIdAsync(model.Id);
                entity.Quantity = model.Quantity;
                await _product.UpdateAsync(entity);
            }
            catch
            {
                return PartialView("_Partials/_EditQuantity", model);
            }
            return Json(new { success = true });
        }
    }
}