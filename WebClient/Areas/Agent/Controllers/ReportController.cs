using BusinessLogic.BindingModel;
using BusinessLogic.BusinessLogic;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using BusinessLogic.ViewModel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Chart = Xceed.Document.NET.Chart;
using Series = Xceed.Document.NET.Series;

namespace WebClient.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class ReportController : Controller
    {
        private  readonly IReisLogic _reis;
        private  readonly IRaionLogic _raions;
        private readonly IDogovorLogic _dogovor;
        private readonly IClientLogic _client;
        public ReportController(IReisLogic reis, IRaionLogic raion, IDogovorLogic dogovor, IClientLogic client)
        {
            _dogovor = dogovor;
            _reis = reis;
            _raions = raion;
            _client = client;
        }

        public IActionResult Report()
        {
            var raions = _raions.Read(null);
           var dogovors = _dogovor.Read(null);
               var  reiss = _reis.Read(null);
            var Texts = new List<string> { };
            var list = new List<List<string>> { };
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            foreach (var raion in raions)
            {
                Texts = new List<string>{
                            raion.Name,
                                           SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 1, 1)),
                                             SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 2, 1)),
                                              SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 3, 1)),
                                           SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 4, 1)),
                                              SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 5, 1)),
                                               SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 6, 1)),
                                               SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 7, 1)),
                                             SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 8, 1)),
                                                 SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 9, 1)),
                                                    SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null), reiss=_reis.Read(null) }, raion, new DateTime(2020, 10, 1)),
                                                       SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 11, 1)),
                                                            SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(null) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 12, 1))};
                list.Add(Texts);
            }
            ViewBag.list = list;
                return View();
        }

        [HttpGet]
        public JsonResult PopulationChart()
        {
                   var populationList = SaveToWord.GetTestDataFirst(new Info
                   {raion=_raions.Read(null),
                   reiss=_reis.Read(null)
                   });
            return Json(populationList);
        }
        public IActionResult Diagramma()
        {
         
            SaveToWord.Diagramma(new Info
            {
                FileName = $"C:\\report-kursovaa\\ReportDiapdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.doc",
                raion = _raions.Read(null),
                reiss = _reis.Read(null)
            });
            return RedirectToAction("Report");
        }
        public IActionResult ReportMonth(string[] Month)
        {
           
            if (Month.Length == 0)
            {
                TempData["ErrorLack"]="Вы не выбрали месяц";
                return RedirectToAction("Report");
            }
            List<string> list = new List<string> { "Номер", "дата", "Клиент", "Сумма"};
            DateTime date = AgentController.PeriodDate(Month[0]);
            SaveToPdf.ReportMonth(new Info
            {
                FileName = $"C:\\report-kursovaa\\ReportMonth{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf",
                Title =$"Отчет о работе Агента {Program.Agent.Id} за месяц {DateTime.Now.Month} года {DateTime.Now.Year}",
                Colon=list,
                dogovors = _dogovor.Read(null),
                Clients=_client.Read(null)
            }, date);
            return RedirectToAction("Report");
        }
     
        /// </summary>
        /// <returns></returns>

        public IActionResult PereReport()
        {
            List<string> list = new List<string> { "район-месяц","01", "02", "03", "04", "05", "06", "07", "08", "09","10","11","12"};
            SaveToPdf.CreateDocPere(new Info
            {
                FileName = $"C:\\report-kursovaa\\ReportPerepdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf",
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name}",
                raion = _raions.Read(null),
                dogovors=_dogovor.Read(null),
                reiss=_reis.Read(null)
            }) ;
            return RedirectToAction("Report");
        }
    }
}
