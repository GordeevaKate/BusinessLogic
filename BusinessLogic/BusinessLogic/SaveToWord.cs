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
        public static void Diagramma(Info info)
        {
            string pathDocument = info.FileName;
            DocX document = DocX.Create(pathDocument);
            document.InsertChart(CreatePieChart(info));
            document.InsertChart(CreateBarChart(info));
            document.Save();
        }
        private static Chart CreatePieChart(Info info)
        {
            PieChart pieChart = new PieChart();
            pieChart.AddLegend(ChartLegendPosition.Left, false);
            pieChart.AddSeries(GetSeriesFirst(info));
            return pieChart;
        }

        private static Chart CreateBarChart(Info info)
        {
            BarChart barChart = new BarChart();
            barChart.AddLegend(ChartLegendPosition.Top, false);
            barChart.AddSeries(GetSeriesFirst(info));
            return barChart;
        }
        public static List<DiagrammaModel> GetTestDataFirst(Info info)
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
        public static List<DiagrammaModel> GetDataDiagramm(Info info)
        {
            List<DiagrammaModel> testDataFirst = new List<DiagrammaModel>();
            foreach (var agent in info.agents)
            {
                double count = 0;
                foreach (var dogovor in info.dogovors)
                {
                    if (agent.Id == dogovor.AgentId)
                        count += dogovor.Summa;
                }
                testDataFirst.Add(new DiagrammaModel() { cityName = agent.Name, summ = count });
            }

            return testDataFirst;
        }
        public static Series GetSeriesFirst(Info info)
        {
            Series seriesFirst = new Series($"Диаграмма {DateTime.Now}");
            if (info.raion != null)
            {
                seriesFirst.Bind(GetTestDataFirst(info), "cityName", "PopulationYear2020");
            }
            seriesFirst.Bind(GetDataDiagramm(info), "cityName", "summ");
            return seriesFirst;
        }
    }
}
