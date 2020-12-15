using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HospitalWebClient.Models;
using КурсоваяBusinessLogic.Interfaces;
using WebClient.Models;
using КурсоваяBusinessLogic.BindingModel;

namespace WebClient.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserLogic _client;
        private readonly IAgentLogic agent;
        public UserController(IUserLogic client, IAgentLogic agent)
        {
            _client = client;
            agent = agent;
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
            if (clientView.Status == 0)
            {
                var agentView = agent.Read(new AgentBindingModel
                {
                    UserId=clientView.Id
                }).FirstOrDefault();
                Program.Agent = agentView;
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
