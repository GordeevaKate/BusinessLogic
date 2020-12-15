using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using КурсоваяBusinessLogic.Interfaces;

namespace WebClient.Controllers
{
    public class ReisController: Controller
    {
        private readonly IReisLogic _doctor;
        public ReisController(IReisLogic reis)
        {
            _doctor = reis;
        }
        public IActionResult Reis()
        {
            ViewBag.Reiss = _doctor.Read(null);
            return View();
        }
    }
}
