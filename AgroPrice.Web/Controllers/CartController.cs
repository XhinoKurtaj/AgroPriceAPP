﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.Product;
using AgroPrice.Services.Cart.Models;
using AgroPrice.Services.Mail;
using AgroPrice.Services.Product.Models;
using AgroPrice.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroPrice.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _products;
        private readonly IMailService _mailService;

        public CartController(IRepository<Product> products, IMailService mailService)
        {
            _products = products;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CartIndexModel();
            var cartSession = HttpContext.Session.Get<List<Item>>("Cart");
            if (cartSession != null)
            {
                model.Items = cartSession;
                model.TotalQuantity = 0;
                model.TotalPrice = 0;
                foreach (var item in cartSession)
                {
                    model.TotalQuantity += item.Quantity;
                    model.TotalPrice += item.Product.Price * item.Quantity;
                }
            }

            return View(model);
        }

        public IActionResult BookProduct(Guid productId, int quantity)
        {
            var cartSession = HttpContext.Session.Get<List<Item>>("Cart");
            if (cartSession == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item()
                {
                    ItemId = Guid.NewGuid(),
                    Product = _products.GetById(productId).ToModel<ProductInCartModel>(),
                    Quantity = 10
                });
                HttpContext.Session.Set("Cart", cart);
            }
            else
            {
                cartSession.Add(new Item()
                {
                    ItemId = Guid.NewGuid(),
                    Product = _products.GetById(productId).ToModel<ProductInCartModel>(),
                    Quantity = 10
                });
                HttpContext.Session.Set("Cart", cartSession);
            }

            return RedirectToAction("Index", "Cart");
        }

        public ActionResult RemoveProduct(Guid itemId)
        {
            var cartSession = HttpContext.Session.Get<List<Item>>("Cart");
            if (cartSession == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                var itemsToSave = new List<Item>();
                
                foreach (var item in cartSession)
                {
                    if (item.ItemId != itemId)
                    {
                        itemsToSave.Add(item);
                    }

                    if (cartSession.Count == 0)
                    {
                        break;
                    }
                }

                cartSession = itemsToSave;
                HttpContext.Session.Set("Cart", cartSession);
                return RedirectToAction("Index", "Cart");
            }
        }

        [HttpGet]
        public async Task<ActionResult> BookWithEmail()
        {
            var cartSession = HttpContext.Session.Get<List<Item>>("Cart");
            var result = await _mailService.CheckoutMessages(cartSession);
            return RedirectToAction("Index", "Home");
        }
    }
}