using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using BusinessLogic.BindingModel;
using WebClient.Areas.Client.Models;
using System;

namespace WebClient.Areas.Client.Controllers
{
    [Area("Client")]
    public class UserController : Controller
    {
        private readonly IUserLogic _client;
        private readonly IAgentLogic _agent;
        public UserController(IUserLogic client, IAgentLogic agent)
        {
            _client = client;
            _agent = agent;
        }
        public ActionResult Profile()
        {
            ViewBag.User = Program.User;
            ViewBag.Agent = Program.Agent;
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
            else
            {
                if (clientView.Status == 0)
                {
                    var agentView = _agent.Read(new AgentBindingModel
                    {
                        UserId = clientView.Id
                    }).FirstOrDefault();
                    Program.Agent = agentView;
                    Program.User = clientView;
                    return RedirectToAction("Profile", "Agent", new { area = "Agent" });
                }
                else//бухгалтер!!!!!
                {
                    if (clientView.Status == BusinessLogic.Enums.UserStatus.Бухгалтер)
                    {
                        var agentView = _agent.Read(new AgentBindingModel
                        {
                            UserId = clientView.Id
                        }).FirstOrDefault();
                        Program.Agent = agentView;
                        Program.User = clientView;
                        return RedirectToAction("Agent", "Agent", new { area = "Buhgalter" });
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            Program.User = null;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Registration(RegistrationModel user)
        {
            if (String.IsNullOrEmpty(user.Login))
            {
                ModelState.AddModelError("", "Введите логин");
                return View(user);
            }
            var existClient = _client.Read(new UserBindingModel
            {
                Login = user.Login
            }).FirstOrDefault();
            if (existClient != null)
            {
                ModelState.AddModelError("", "Уже есть клиент с таким логином");
                return View(user);
            }
            if (user.Password.Length > 50 ||
            user.Password.Length < 8)
            {
                ModelState.AddModelError("", $"Длина пароля должна быть от {8} до {50} символов");
                return View(user);
            }
            /*if (String.IsNullOrEmpty(user.ClientFIO))
            {
                ModelState.AddModelError("", "Введите ФИО");
                return View(user);
            }*/
            if (String.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(user);
            }
            _client.CreateOrUpdate(new UserBindingModel
            {
                Login = user.Login,
                Password = user.Password,
                Status = 0
            });
            var id = _client.Read(null).Where(rec => rec.Login == user.Login).FirstOrDefault();
            _agent.CreateOrUpdate(new AgentBindingModel
            {
                Name = user.ClientFIO,
                Oklad = user.Oklad,
                UserId = id.Id,
                Comission = user.Comission
            });
            ModelState.AddModelError("", "Вы успешно зарегистрированы");
            return View("Registration", user);
        }
    }
}