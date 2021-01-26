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
		public IActionResult Zarplata()
		{
			return View();
		}
	}
}
