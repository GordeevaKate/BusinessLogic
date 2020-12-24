using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
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
        public ReportController(IReisLogic reis, IRaionLogic raion)
        {
            _reis = reis;
            _raions = raion;
        }
        public ActionResult Report()
        {
            return View();
        }

        public IActionResult Diagramma()
        {
            string pathDocument = $"C:\\report-kursovaa\\ReportClientpdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.doc";
            DocX document = DocX.Create(pathDocument);
            document.InsertChart(CreatePieChart());
            document.InsertChart(CreateBarChart());
            document.Save();
            return RedirectToAction("Report");
        }
        private  Chart CreatePieChart()
        {
            PieChart pieChart = new PieChart();
            pieChart.AddLegend(ChartLegendPosition.Left, false);
            pieChart.AddSeries(GetSeriesFirst());
            return pieChart;
        }

        private  Chart CreateBarChart()
        {       
            BarChart barChart = new BarChart();
            barChart.AddLegend(ChartLegendPosition.Top, false);
            barChart.AddSeries(GetSeriesFirst());
            return barChart;
        }
        private  List<ReisBindingModel> GetTestDataFirst()
        {
            List<ReisBindingModel> testDataFirst = new List<ReisBindingModel>();
            var raions = _raions.Read(null);
            foreach (var raion in raions)
            {
                testDataFirst.Add(new ReisBindingModel() { Name = raion.Name, Cena = _reis.Read(new ReisBindingModel { OfId = (int)raion.Id }).Count });
            }

            return testDataFirst;
        }
        public  Series GetSeriesFirst()
        {
            Series seriesFirst = new Series("Диаграмма");
            seriesFirst.Bind(GetTestDataFirst(), "Name", "Cena");
            return seriesFirst;
        }


        public IActionResult PereReport()
        {
            List<string> list = new List<string> { "", "", "", "", "", "", "", "", ",", "," };
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = $"C:\\report-kursovaa\\ReportPerepdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf",
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name}",
              //  Clients = clients
            });
            return RedirectToAction("Report");
        }
    }
}
