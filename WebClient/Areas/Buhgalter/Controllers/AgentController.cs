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
        public static DateTime PeriodDate(string Month)
        {
            DateTime date;
            switch (Month)
            {
                case "Январь":
                    date = new DateTime(2020, 1, 1);
                    break;
                case "Февраль":
                    date = new DateTime(2020, 2, 1);
                    break;
                case "Март":
                    date = new DateTime(2020, 3, 1);
                    break;
                case "Май":
                    date = new DateTime(2020, 4, 1);
                    break;
                case "Апрель":
                    date = new DateTime(2020, 5, 1);
                    break;
                case "Июнь":
                    date = new DateTime(2020, 6, 1);
                    break;
                case "Июль":
                    date = new DateTime(2020, 7, 1);
                    break;
                case "Август":
                    date = new DateTime(2020, 8, 1);
                    break;
                case "Сентябрь":
                    date = new DateTime(2020, 9, 1);
                    break;
                case "Октябрь":
                    date = new DateTime(2020, 10, 1);
                    break;
                case "Ноябрь":
                    date = new DateTime(2020, 11, 1);
                    break;
                case "Декабрь":
                    date = new DateTime(2020, 12, 1);
                    break;
                default:
                    date = new DateTime(2020, 2, 1);
                    break;
            }
            return date;
        }
    }
}
