using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroPrice.Web.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public abstract class AdminController : Controller
    {

    }
}