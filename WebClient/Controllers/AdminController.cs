using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HospitalBusinessLogic.BindingModel;
using HospitalBusinessLogic.Interfaces;
using HospitalWebClient.Models;

namespace HospitalWebClient.Controllers
{
    public class AdminController : Controller
    {
        private string password = "qwerty";
        private readonly IClientLogic _client;

        public AdminController(IClientLogic client)
        {
            _client = client;
        }
        public IActionResult Index(AdminModel model)
        {
            if (model.Password == password)
            {
                Program.AdminMode = !Program.AdminMode;
                return RedirectToAction("Blocking");
            }
            else if (model.Password != password && model.Password != null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль");
                return View();
            }
            return View();
        }
        public IActionResult Blocking()
        {
            ViewBag.Clients = _client.Read(null);
            return View();
        }
        public ActionResult Block(int id)
        {
            if (ModelState.IsValid)
            {
                var existClient = _client.Read(new ClientBindingModel
                {
                    Id = id
                }).FirstOrDefault();
                _client.CreateOrUpdate(new ClientBindingModel
                {
                    Id = id,
                    ClientFIO = existClient.ClientFIO,
                    Login = existClient.Login,
                    Password = existClient.Password,
                    Email = existClient.Email,
                    PhoneNumber = existClient.PhoneNumber,
                    Block = !existClient.Block
                });
                return RedirectToAction("Blocking");
            }
            return RedirectToAction("Blocking");
        }
        public IActionResult Logout()
        {
            Program.AdminMode = false;
            return RedirectToAction("Index", "Home");
        }
    }
}