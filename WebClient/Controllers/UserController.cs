using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models;
using System.Text.RegularExpressions;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяBusinessLogic.BindingModel;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class UserController
    {
        private readonly IUserLogic _client;
        private readonly int passwordMinLength = 6;
        private readonly int passwordMaxLength = 20;
        private readonly int loginMinLength = 1;
        private readonly int loginMaxLength = 50;
        public UserController(IUserLogic client)
        {
            _client = client;
        }
        public ActionResult Profile()
        {
            ViewBag.User = Program.User;
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel client)
        {
            var clientView = _client.Read(new UserBindingModel
            {
                Login = client.Login,
                Password = client.Password
            }).FirstOrDefault();
            if (clientView == null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль, либо пользователь не найден");
                return View(client);
            }
            Program.User = clientView;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            Program.User = null;
            return RedirectToAction("Index", "Home");
        }

    }
}
