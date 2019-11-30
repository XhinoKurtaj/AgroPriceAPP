using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroPrice.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PointOfSaleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}