using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Services.Product;
using AgroPrice.Services.Product.Models;
using AgroPrice.Services.WholeSaleMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IRepository<Product> _product;
        private readonly IRepository<ProductDetails> _productDetails;

        public ProductController(IProductService productService, IRepository<Product> product, IRepository<ProductDetails> productDetails)
        {
            _productService = productService;
            _product = product;
            _productDetails = productDetails;
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
        public async Task<IActionResult> EditProductDetails(Guid id)
        {
            var maxDate = await _productDetails.TableNoTracking.Where(x => x.ProductId == id).MaxAsync(y => y.ModificationDate);
            var entity = await _productDetails.TableNoTracking.Where(x => x.ProductId == id).FirstOrDefaultAsync(y => y.ModificationDate == maxDate);
            var model = new EditQuantityModel
            {
                Id = id,
                Quantity = entity.Quantity,
                Price = entity.Price
            };
            return PartialView("_Partials/_EditProductDetails", model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProductDetails(EditQuantityModel model)
        {
            if (!ModelState.IsValid) 
            {
                return RedirectToAction("EditProductDetails", new { id = model.Id });
            }

            var result = await _productService.RegisterProductDetails(model);
            if (!result.Success)
            {
                return RedirectToAction("EditProductDetails", new { id = model.Id });
            }
            return Json(new { success = true });
        }

    }
}