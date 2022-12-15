using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HouseManagementApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}