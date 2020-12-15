using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HospitalBusinessLogic.BindingModel;
using HospitalBusinessLogic.Interfaces;
using HospitalWebClient.Models;
using System.Text.RegularExpressions;

namespace HospitalWebClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientLogic _client;
        private readonly int passwordMinLength = 6;
        private readonly int passwordMaxLength = 20;
        private readonly int loginMinLength = 1;
        private readonly int loginMaxLength = 50;
        public ClientController(IClientLogic client)
        {
            _client = client;
        }
        public ActionResult Profile()
        {
            ViewBag.User = Program.Client;
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel client)
        {
            var clientView = _client.Read(new ClientBindingModel
            {
                Login = client.Login,
                Password = client.Password
            }).FirstOrDefault();
            if (clientView == null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль, либо пользователь не найден");
                return View(client);
            }
            if (clientView.Block == true)
            {
                ModelState.AddModelError("", "Пользователь заблокирован");
                return View(client);
            }
            Program.Client = clientView;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            Program.Client = null;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Registration(RegistrationModel client)
        {
            if (String.IsNullOrEmpty(client.Login))
            {
                ModelState.AddModelError("", "Введите логин");
                return View(client);
            }
            if (client.Login.Length > loginMaxLength ||
           client.Login.Length < loginMinLength)
            {
                ModelState.AddModelError("", $"Длина логина должна быть от {loginMinLength} до {loginMaxLength} символов");
                return View(client);
            }
            var existClient =_client.ReadofRegistration(new ClientBindingModel
                {
                    Login = client.Login
                }).FirstOrDefault();
                if (existClient != null)
                {
                    ModelState.AddModelError("", "Данный логин уже занят");
                    return View(client);
                }
            if (String.IsNullOrEmpty(client.Email))
            {
                ModelState.AddModelError("", "Введите электронную почту");
                return View(client);
            }
            existClient = _client.ReadofRegistration(new ClientBindingModel
                {  
                    Email = client.Email
                }).FirstOrDefault();
                if (existClient != null)
                {
                    ModelState.AddModelError("", "Данный E-Mail уже занят");
                    return View(client);
                }
            if (!Regex.IsMatch(client.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                ModelState.AddModelError("", "Email введен некорректно");
                return View(client);
            }
            if (client.Password.Length > passwordMaxLength ||
            client.Password.Length < passwordMinLength)
            {
                ModelState.AddModelError("", $"Длина пароля должна быть от {passwordMinLength} до {passwordMaxLength} символов");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.ClientFIO))
            {
                ModelState.AddModelError("", "Введите ФИО");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.PhoneNumber))
            {
                ModelState.AddModelError("", "Введите номер телефона");
                return View(client);
            }
            existClient = _client.ReadofRegistration(new ClientBindingModel
                {
                    PhoneNumber = client.PhoneNumber
                }).FirstOrDefault();
                if (existClient != null)
                {
                    ModelState.AddModelError("", "Данный телефон уже занят");
                    return View(client);
                }
                _client.CreateOrUpdate(new ClientBindingModel
                {
                    ClientFIO = client.ClientFIO,
                    Login = client.Login,
                    Password = client.Password,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    Block = false
                }) ;
                ModelState.AddModelError("", "Вы успешно зарегистрированы");
                return View("Registration", client);
            }
    }
}