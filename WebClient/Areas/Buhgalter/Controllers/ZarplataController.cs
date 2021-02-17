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
		private readonly IZarplataLogic _zarplata;
		private double zp = 0;
		private double comis = 0;
		public ZarplataController(IAgentLogic agent, IDogovorLogic dogovor, IZarplataLogic zarplata)
		{
			_zarplata = zarplata;
			_agent = agent;
			_dogovor = dogovor;
		}
		public IActionResult Zarplata(int id, string[] Month, string valueINeed)
		{
			bool check = false;
			if (valueINeed == "Рассчет за год")
			{
				check = true;
			}
			var summ = _agent.Read(new AgentBindingModel { Id = id }).FirstOrDefault();
			zp = summ.Oklad;
			comis = summ.Comission;
			if (Month.Length != 0)
				zp = ResultZp(Month, id, check);
			ViewBag.Zp = zp;
			return View();

		}
		private double ResultZp(string[] Month, int id, bool check)
		{
			if (Month.Length == 0)
			{
				TempData["ErrorLack"] = "Вы не выбрали месяц";
				return 0;
			}
			double dogovor = 0;
			if (check)
			{
				dogovor = _dogovor.Read(null).Where(rec => (rec.data > DateTime.Now.AddYears(-1) && rec.data < DateTime.Now) && (rec.AgentId == id)).Select(rec => rec.Summa).Sum();
				zp *= 12;
			}
			else
			{
				DateTime date = AgentController.PeriodDate(Month[0]);
				dogovor = _dogovor.Read(null).Where(rec => (rec.data.Month ==date.Month) && (rec.AgentId == id)).Select(rec => rec.Summa).Sum();
			}
			zp += dogovor * (comis/100);
			return zp;
		}
		public IActionResult Statements(string[] Month, ZarplataModel model)
		{
			List<ZarplataModel> inf = new List<ZarplataModel>();
			if (Month.Length == 0)
			{
				ViewBag.inf = _zarplata.Read(null);
				return View();
			}
			var agent = _agent.Read(null).Last();
			for (int i = 1; i <= agent.Id; i++)
			{
				var ag = _agent.Read(new AgentBindingModel { Id = i }).FirstOrDefault();
				if (ag != null)
				{
					bool check = false;
					foreach (var zp in _zarplata.Read(null))
					{
						if (zp.data == AgentController.PeriodDate(Month[0]) && zp.UserId == ag.Id)
							check = true;
					}
					var summ = _agent.Read(new AgentBindingModel { Id = i }).FirstOrDefault();
					zp = summ.Oklad;
					var com = _agent.Read(new AgentBindingModel { Id = i }).FirstOrDefault();
					comis = com.Comission;
					inf.Add(new ZarplataModel
					{
						Id = i,
						Name = ag.Name,
						Summa = ResultZp(Month, i, false)
					});
					if (!check)
					{
						_zarplata.CreateOrUpdate(new ZarplataBindingModel
						{
							UserId = i,
							Name = ag.Name,
							Summa = ResultZp(Month, i, false),
							data = AgentController.PeriodDate(Month[0])
						});
					}
				}
			}
			ViewBag.inf = inf;
			return View();
		}

	}
}
