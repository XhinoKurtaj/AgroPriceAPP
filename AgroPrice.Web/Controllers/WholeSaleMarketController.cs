using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroPrice.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class WholeSaleMarketController : Controller
    {
        private readonly IWholeSaleMarketService _wholeSaleMarketService;
        private readonly IRepository<WholeSaleMarket> _wholeSaleMarket;

        public WholeSaleMarketController(IWholeSaleMarketService wholeSaleMarketService, IRepository<WholeSaleMarket> wholeSaleMarket)
        {
            _wholeSaleMarketService = wholeSaleMarketService;
            _wholeSaleMarket = wholeSaleMarket;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _wholeSaleMarketService.GetAllWholeSaleMarkets();
            return View(result);
        }
        
    }
}