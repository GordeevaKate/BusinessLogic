using System.Linq;
using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Areas.Buhgalter.Models;
using System;

namespace WebClient.Areas.Buhgalter.Controllers
{
    [Area("Buhgalter")]
    public class ReisController : Controller
	{
        private readonly IDogovorLogic _dogovor;
        private readonly IAgentLogic _agent;
        private readonly IReisLogic _reis;
        private readonly IRaionLogic _raions;
        public ReisController(IAgentLogic agent, IRaionLogic raion, IReisLogic reis, IDogovorLogic dogovor)
        {
            _agent = agent;
            _dogovor = dogovor;
            _reis = reis;
            _raions = raion;
        }
        public ActionResult Reis()
        {
           	ViewBag.Dogovors = _dogovor.Read(null);
			return View();
		}
        [HttpPost]
        public ActionResult ReisChange(int? id, double percent)
        {
            var sum = _dogovor.Read(new DogovorBindingModel
            {
                Id = id
            }).FirstOrDefault();
            _dogovor.CreateOrUpdate(new DogovorBindingModel
            {
                Id = id,
                Summa = percent/100 * sum.Summa + sum.Summa
            });
            return RedirectToAction("Reis");
        }
    }
}
