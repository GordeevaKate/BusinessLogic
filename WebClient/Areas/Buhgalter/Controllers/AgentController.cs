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
            ViewBag.Agent = _agent.Read(null);
            return View();
        }
	}
}
