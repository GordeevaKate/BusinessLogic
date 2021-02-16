using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLogic.BindingModel;
using BusinessLogic.BusinessLogic;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using Microsoft.AspNetCore.Mvc;
using WebClient.Areas.Buhgalter.Models;

namespace WebClient.Areas.Buhgalter.Controllers
{
    [Area("Buhgalter")]
    public class ReportController : Controller
	{
        private readonly IDogovorLogic _dogovor;
        private readonly IAgentLogic _agent;
        private readonly IZarplataLogic _zarplata;
        public ReportController(IAgentLogic agent, IDogovorLogic dogovor, IZarplataLogic zarplata)
        {
            _zarplata = zarplata;
            _agent = agent;
            _dogovor = dogovor;
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
        public IActionResult ReadOfDiagramma(ReportModel model)
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
        [HttpGet]
        public IActionResult ReadOfAgent(string[] Month, ReportModel m)
        {

            if (Month.Length == 0)
            {

                TempData["ErrorLack"] = "Вы не выбрали месяц";
                return RedirectToAction("Report");
            }
            var FileName = m.puth + $"ReportMonth{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf";
            List<string> list = new List<string> { "Номер", "дата", "Клиент", "Сумма" };
            DateTime date = AgentController.PeriodDate(Month[0]);
            SaveToPdf.ZpMonth(new Info
            {
                FileName = FileName,
                Title = $"Отчет зп за месяц {DateTime.Now.Month} года {DateTime.Now.Year}",
                Colon = list,
                zarplatas = _zarplata.Read(null).Where(rec => rec.data.Month == date.Month).ToList()
            }, date); ;
            Mail.SendMail(m.SendMail, FileName, $"Отчет о работе Агента {Program.Agent.Id} за месяц {DateTime.Now.Month} года {DateTime.Now.Year}");
            return RedirectToAction("Report");
        }
        public IActionResult SendPuth(ReportModel model, int id, int did, string[] Month)
        {
            ViewBag.Id = id;
            ViewBag.Month = Month;
            if (did != null) { ViewBag.Did = did; }
            else { ViewBag.Did = 0; }
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            return View();
        }
        public IActionResult Validation(ReportModel model1, int id, int did, string[] Month)
        {
            if (!Directory.Exists(model1.puth))
            {
                TempData["ErrorLack"] = "На данном компьютере не существует такого пути";
                return RedirectToAction("SendPuth", "Report", new { id = id, puth = model1.puth, SendMail = model1.SendMail, dis = did, Month = Month });
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
                return RedirectToAction("ReadOfReportSpisok", "Client", model1);
            }
            if (id == 2)
            {
                return RedirectToAction("Report", "Dogovor", new
                {

                    SendMail = model1.SendMail,
                    puth = model1.puth,

                    dogovorid = did
                });
            }
            if (id == 3)
            {
                return RedirectToAction("ReadOfAgent", new
                {
                    SendMail = model1.SendMail,
                    puth = model1.puth,
                    Month
                });
            }
            if (id == 4)
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
