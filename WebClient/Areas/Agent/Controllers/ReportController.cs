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
using System.IO;
using System.Text.RegularExpressions;
using WebClient.Areas.Agent.Models;
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
                                           SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 1, 1)),
                                             SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 2, 1)),
                                              SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 3, 1)),
                                           SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 4, 1)),
                                              SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 5, 1)),
                                               SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 6, 1)),
                                               SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 7, 1)),
                                             SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 8, 1)),
                                                 SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 9, 1)),
                                                    SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}), reiss=_reis.Read(null) }, raion, new DateTime(2020, 10, 1)),
                                                       SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 11, 1)),
                                                            SaveToPdf.Count(new Info{ dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}) , reiss=_reis.Read(null)}, raion, new DateTime(2020, 12, 1))};
                list.Add(Texts);
            }
            ViewBag.list = list;
                return View();
        }

        [HttpGet]
        public JsonResult Metod()
        {
                   var populationList = SaveToWord.GetTestDataFirst(new Info
                   {raion=_raions.Read(null),
                   reiss=_reis.Read(null)
                   });

            return Json(populationList);
        }
        public IActionResult ReadOfDiagramma(ReportViewModel model)
        {
         
            SaveToWord.Diagramma(new Info
            {
                Title = $"Даграмма за перевозки за {DateTime.Now.Year}",
                FileName = model.puth+ $"ReportDiapdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.doc",
                raion = _raions.Read(null),
                reiss = _reis.Read(null)
            });
            Mail.SendMail(model.SendMail, model.puth + $"ReportDiapdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.doc", $"Диаграмма");
            return RedirectToAction("Report");
        }
        [HttpGet]
        public IActionResult ReadOfAgent(string[] Month, ReportViewModel m)
        {
           
            if (Month.Length == 0)
            {

                TempData["ErrorLack"]="Вы не выбрали месяц";
                return RedirectToAction("Report");
            }
            var FileName = m.puth + $"ReportMonth{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf";
            List<string> list = new List<string> { "Номер", "дата", "Клиент", "Сумма"};
            DateTime date = AgentController.PeriodDate(Month[0]);
            SaveToPdf.ReportMonth(new Info
            {
                FileName =FileName,
                Title =$"Отчет о работе Агента {Program.Agent.Id} за месяц {DateTime.Now.Month} года {DateTime.Now.Year}",
                Colon=list,
                dogovors = _dogovor.Read(null),
                Clients=_client.Read(null)
            }, date);
                Mail.SendMail(m.SendMail, FileName, $"Отчет о работе Агента {Program.Agent.Id} за месяц {DateTime.Now.Month} года {DateTime.Now.Year}");
            return RedirectToAction("Report");
        }
     
        /// </summary>
        /// <returns></returns>

        public IActionResult ReadOfPereReport(ReportViewModel model)
        {
            List<string> list = new List<string> { "район-месяц","01", "02", "03", "04", "05", "06", "07", "08", "09","10","11","12"};
            var FileName =model.puth + $"ReportPerepdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf";
            SaveToPdf.CreateDocPere(new Info
            {
                FileName =FileName,
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name} по неархивированным заключенным договорам{DateTime.Now}",
                raion = _raions.Read(null),
                dogovors=_dogovor.Read(new DogovorBindingModel { AgentId= (int)Program.Agent.Id}),
                reiss=_reis.Read(null)
            }) ;

            Mail.SendMail(model.SendMail, FileName, $"Отчет о работе Агента {Program.Agent.Id} за месяц {DateTime.Now.Month} года {DateTime.Now.Year}");
            return RedirectToAction("Report");
        }

        public IActionResult SendPuth(ReportViewModel model, int id, int did, string[] Month)
        {
            ViewBag.Id = id;
            ViewBag.Month = Month;
            if (did != null) { ViewBag.Did = did; }
            else { ViewBag.Did = 0;}
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Validation(ReportViewModel model1,int id, int did, string[] Month)
        {
            if (!Directory.Exists(model1.puth))
            {
                TempData["ErrorLack"] = "На данном компьютере не существует такого пути";
                return RedirectToAction("SendPuth", "Report",new{id=id,puth=model1.puth, SendMail=model1.SendMail, dis= did, Month=Month } );
            }
            if (String.IsNullOrEmpty(model1.SendMail))
            {
                TempData["ErrorLack"] = "Вы не ввели почту";
                return RedirectToAction("SendPuth", "Report", new { id = id, puth = model1.puth, SendMail = model1.SendMail, dis = did, Month = Month });
            }
            if (!Regex.IsMatch(model1.SendMail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                TempData["ErrorLack"] = "Почта введена некоретно";
                return RedirectToAction("SendPuth", "Report", new { id = id, puth = model1.puth, SendMail = model1.SendMail, dis = did, Month = Month });
            }
            if (id == 1)
            {
                return RedirectToAction("ReadOfReportSpisok", "Client",model1);
            }
            if (id == 2)
            {
                return RedirectToAction("Report", "Dogovor", new
                {
                  
                   SendMail=model1.SendMail,
                    puth = model1.puth,

                    dogovorid =  did
                }  );
            }
            if (id== 3)
            {
                return RedirectToAction("ReadOfAgent", new
                {
                    SendMail = model1.SendMail, puth = model1.puth , Month
                });
            }
          if(id==4)
            {
                return RedirectToAction("ReadOfPereReport", new
                {
                    SendMail = model1.SendMail,
                    puth = model1.puth       
                });
            }
            if (id == 5)
            {
                return RedirectToAction("ReadOfDiagramma", new
                {
                    SendMail = model1.SendMail,
                    puth = model1.puth
                });
            }

            return RedirectToAction("Archivation", "Archive", new
            {
                SendMail = model1.SendMail,
                puth = model1.puth
            });

        }
    }
}
