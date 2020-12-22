using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Agent.Controllers
{

        [Area("Agent")]
        public class HomeController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
        }

}
