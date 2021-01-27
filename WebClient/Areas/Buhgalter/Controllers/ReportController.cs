using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Areas.Buhgalter.Controllers
{
    [Area("Buhgalter")]
    public class ReportController : Controller
	{
        private readonly IDogovorLogic _dogovor;
        private readonly IAgentLogic _agent;
        private readonly IReisLogic _reis;
        private readonly IRaionLogic _raion;
        public ReportController(IAgentLogic agent, IRaionLogic raion, IReisLogic reis, IDogovorLogic dogovor)
        {
            _agent = agent;
            _dogovor = dogovor;
            _reis = reis;
            _raion = raion;
        }
        public IActionResult Report()
        {
            return View();
        }
        public IActionResult Dogovors()
		{
            ViewBag.Dogovors = _dogovor.Read(null);
            return View();
        }
	}
}
