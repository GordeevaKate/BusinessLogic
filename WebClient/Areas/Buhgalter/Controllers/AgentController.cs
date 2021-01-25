using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebClient.Areas.Buhgalter.Models;

namespace WebClient.Areas.Buhgalter.Controllers
{
    [Area("Buhgalter")]
    public class AgentController : Controller
	{
        private readonly IAgentLogic _agent;
        public AgentController(IAgentLogic agent)
        {
            _agent = agent;
        }
        public IActionResult Agent(AgentListModel model)
		{
            if (model.FIO!= null)
            {              
                var agent = _agent.Read(new AgentBindingModel { Name = model.FIO });
                ViewBag.Agent = agent;
                if (agent.Count == 0)
                {
                    ModelState.AddModelError("FIO", "Агента не существует");
                    ViewBag.Agent = _agent.Read(null);
                    return View();
                }
                return View();
            }
            if (model.Id != null)
            {
                var agent = _agent.Read(new AgentBindingModel { Id = model.Id });
                ViewBag.Agent = agent;
                if (agent.Count == 0)
                {
                    ModelState.AddModelError("Id", "Агента не существует");
                    ViewBag.Agent = _agent.Read(null);
                    return View();
                }
                return View();
            }
            /*if (Validation(model.Obem) == true)
            {

                var clients = _client.Read(null);
                List<ClientViewModel> listclient = new List<ClientViewModel> { };
                foreach (var client in clients)
                {
                    double obem = 0;
                    var dogovorss = _dogovor.Read(null);
                    var dogovors = _dogovor.Read(new DogovorBindingModel
                    {
                        ClientId = client.Id,
                        AgentId = (int)Program.Agent.Id

                    });
                    foreach (var dogovor in dogovors)
                    {
                        foreach (var reis in dogovor.Dogovor_Reiss)
                        {
                            obem += reis.Value.Item6;
                        }
                    }
                    if (obem >= Convert.ToDouble(model.Obem))
                    {
                        listclient.Add(client);
                    }
                }
                ViewBag.Client = listclient;
                return View();
            }
            else
            {
                if (model.Obem != "0")
                    ModelState.AddModelError("Obem", "Объем введен неправильно");
            }*/
            ViewBag.Agent = _agent.Read(null);
            return View();
        }
	}
}
