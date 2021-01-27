﻿using System;
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
		public IActionResult Zarplata(int id, string[] Month, string valueINeed)
		{
			bool check = false;
			if (valueINeed == "Рассчет за год")
			{
				check = true;
			}
			var summ = _agent.Read(new AgentBindingModel { Id = id }).FirstOrDefault();
			zp = summ.Oklad;
			var com = _agent.Read(new AgentBindingModel { Id = id }).FirstOrDefault();
			comis = com.Comission;
			if (Month.Length != 0)
				zp = ResultZp(Month, id, check);
			ViewBag.Zp = zp;
			return View();

		}
		private double ResultZp(string[] Month, int id, bool check)
		{
			double dogovor = 0;
			if (check)
			{
				dogovor = _dogovor.Read(null).Where(rec => (rec.data > DateTime.Now.AddYears(-1)) && (rec.AgentId == id)).Select(rec => rec.Summa).Sum();
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
				return View();
			var agent = _agent.Read(null).Last();
			for (int i = 1; i <= agent.Id; i++)
			{
				var ag = _agent.Read(new AgentBindingModel { Id = i }).FirstOrDefault();
				if (ag != null)
				{
					var summ = _agent.Read(new AgentBindingModel { Id = i }).FirstOrDefault();
					zp = summ.Oklad;
					var com = _agent.Read(new AgentBindingModel { Id = i }).FirstOrDefault();
					comis = com.Comission;
					inf.Add(new ZarplataModel
					{
						Id = i,
						Name = ag.Name,
						Summ = ResultZp(Month, i, false)
					});	
				}
			}
			ViewBag.inf = inf;
			return View();
		}

	}
}
