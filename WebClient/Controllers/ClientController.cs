using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.Interfaces;

namespace WebClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientLogic _client;
            private readonly IDogovorLogic _dogovor;
        public ClientController(IClientLogic client, IDogovorLogic dogovor)
        {
            _client = client;
            _dogovor = dogovor;
        }

        public IActionResult Client()
        {
            var agent = Program.Agent;
            ViewBag.Client = _client.Read(new ClientBindingModel { UserId= (int)agent.Id} );
            return View();
        }
        public IActionResult Dogovor(int? id)
        {
            Program.ClientId = (int)id;
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Dogovors = _dogovor.Read(new DogovorBindingModel
            {
                ClientId = (int)id,
                AgentId= (int)Program.Agent.Id
            });

            return View();
        }

     
    }
}
