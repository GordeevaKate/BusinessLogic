using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using BusinessLogic.ViewModel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Chart = Xceed.Document.NET.Chart;
using Series = Xceed.Document.NET.Series;
namespace BusinessLogic.BusinessLogic
{
  public  class SaveToWord
    {
        public static void Diagramma(PdfInfo info)
        {
            string pathDocument =info.FileName;
            DocX document = DocX.Create(pathDocument);
            document.InsertChart(CreatePieChart(info));
            document.InsertChart(CreateBarChart(info));
            document.Save();
        }
        private static Chart CreatePieChart(PdfInfo info)
        {
            PieChart pieChart = new PieChart();
            pieChart.AddLegend(ChartLegendPosition.Left, false);
            pieChart.AddSeries(GetSeriesFirst(info));
            return pieChart;
        }

        private static Chart CreateBarChart(PdfInfo info)
        {
            BarChart barChart = new BarChart();
            barChart.AddLegend(ChartLegendPosition.Top, false);
            barChart.AddSeries(GetSeriesFirst(info));
            return barChart;
        }
        public static List<DiagrammaModel> GetTestDataFirst(PdfInfo info)
        {
            List<DiagrammaModel> testDataFirst = new List<DiagrammaModel>();
            foreach (var raion in info.raion)
            {  int count = 0;
                foreach(var reis in info.reiss)
                {
                  
                    if (reis.OfId == raion.Id || reis.ToId == raion.Id)
                    {
                        count++;
                    }
                }
                testDataFirst.Add(new DiagrammaModel() { cityName = raion.Name, PopulationYear2020 = count });
            }

            return testDataFirst;
        }
        public static Series GetSeriesFirst(PdfInfo info)
        {
            Series seriesFirst = new Series("Диаграмма");
            seriesFirst.Bind(GetTestDataFirst(info), "cityName", "PopulationYear2020");
            return seriesFirst;
        }
    }
}
