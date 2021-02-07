using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.BindingModel;
using BusinessLogic.BusinessLogic;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebClient.Areas.Agent.Models;

namespace WebClient.Areas.Buhgalter.Controllers
{
    [Area("Buhgalter")]
    public class ReportController : Controller
	{
        private readonly IDogovorLogic _dogovor;
        private readonly IAgentLogic _agent;
        private readonly IReisLogic _reis;
        private readonly IRaionLogic _raions;
        public ReportController(IAgentLogic agent, IRaionLogic raion, IReisLogic reis, IDogovorLogic dogovor)
        {
            _agent = agent;
            _dogovor = dogovor;
            _reis = reis;
            _raions = raion;
        }
        public IActionResult Report(int id)
        {
            return View();
        }
        public IActionResult Dogovors()
		{
            ViewBag.Dogovors = _dogovor.Read(null);
            return View();
        }
        [HttpGet]
        public JsonResult Metod()
        {
            var populationList = SaveToWord.GetDataDiagramm(new Info
            {
                dogovors = _dogovor.Read(null),
                agents = _agent.Read(null)
            });

            return Json(populationList);
        }
        public IActionResult ReadOfDiagramma(ReportViewModel model)
        {

            SaveToWord.Diagramma(new Info
            {
                Title = $"Даграмма - стоимость заключенных договоров по агентам;",
                FileName = model.puth + $"ReportDiapdf.doc",
                agents = _agent.Read(null),
                dogovors = _dogovor.Read(null)
            });
            Mail.SendMail(model.SendMail, model.puth + $"ReportDiapdf.doc", $"Диаграмма");
            return RedirectToAction("Report");
        }
    }
}
