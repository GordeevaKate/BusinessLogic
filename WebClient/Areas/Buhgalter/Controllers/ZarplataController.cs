using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Areas.Buhgalter.Models;


namespace WebClient.Areas.Buhgalter.Controllers
{
	[Area("Buhgalter")]
	public class ZarplataController : Controller
	{
		private readonly IAgentLogic _agent;
		private readonly IDogovorLogic _dogovor;
		private double zp = 0;
		private double comis = 0;
		public ZarplataController(IAgentLogic agent, IDogovorLogic dogovor)
		{
			_agent = agent;
			_dogovor = dogovor;
		}
		public IActionResult Zarplata(int id, string[] Month, int check)
		{
			if (check == 123)
			{
				ViewBag.Zp = "долбаеб";
				return View();
			}
			var summ = _agent.Read(new AgentBindingModel { Id = id }).FirstOrDefault();
			zp = summ.Oklad;
			var com = _agent.Read(new AgentBindingModel { Id = id }).FirstOrDefault();
			comis = com.Comission;
			if (Month.Length != 0)
				zp = ResultZp(Month, id);
			ViewBag.Zp = zp;
			return View();

		}
		private double ResultZp(string[] Month, int id)
		{
			double dogovor = 0;
			if (Month.Length == 12)
			{
				DateTime date = new DateTime();
				dogovor = _dogovor.Read(null).Where(rec => (rec.data > DateTime.Now.AddYears(-1)) && (rec.AgentId == id)).Select(rec => rec.Summa).Sum();
			}
			else
			{
				DateTime date = AgentController.PeriodDate(Month[0]);
				dogovor = _dogovor.Read(null).Where(rec => (rec.data.Month == date.Month) && (rec.AgentId == id)).Select(rec => rec.Summa).Sum();
			}
			zp += dogovor * (comis/100);
			return zp;
		}

	}
}
