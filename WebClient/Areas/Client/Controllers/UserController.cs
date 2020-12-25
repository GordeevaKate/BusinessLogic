﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using BusinessLogic.BindingModel;
using WebClient.Areas.Client.Models;

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
    }
}