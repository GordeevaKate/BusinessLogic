
using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace WebClient.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class AgentController : Controller
    {
        private readonly IUserLogic _client;
        private readonly IAgentLogic _agent;
        private readonly IDogovorLogic _dogovor;


        public AgentController(IUserLogic client,IAgentLogic agent, IDogovorLogic dogovor)
        {
            _client = client;
            _agent = agent;
            _dogovor = dogovor;
        }
        public IActionResult Profile(string[] Month)
        {
            try {
                DateTime date=  PeriodDate(Month[0]);
            var dogovors = _dogovor.Rascet(Program.Agent.Id, date);
            double zarplata = Program.Agent.Oklad;
            foreach(var dogovor in dogovors)
            {
                zarplata += dogovor.Summa * Program.Agent.Comission;
            }
            ViewBag.Zarplata=zarplata; 
            }
            catch
            {
                ViewBag.Zarplata = "";
            }
            ViewBag.User = Program.User;
            ViewBag.Agent = Program.Agent;
            return View();
        }
        public DateTime PeriodDate(string Month)
        {
            DateTime date;
            switch (Month)
            {
                case "Январь":
                    date = new DateTime(2020, 1, 1);
                    break;
                case "Февраль":
                    date = new DateTime(2020, 2, 1);
                    break;
                case "Март":
                    date = new DateTime(2020, 3, 1);
                    break;
                case "Май":
                    date = new DateTime(2020, 4, 1);
                    break;
                case "Апрель":
                    date = new DateTime(2020, 5, 1);
                    break;
                case "Июнь":
                    date = new DateTime(2020, 6, 1);
                    break;
                case "Июль":
                    date = new DateTime(2020, 7, 1);
                    break;
                case "Август":
                    date = new DateTime(2020, 8, 1);
                    break;
                case "Сентябрь":
                    date = new DateTime(2020, 9, 1);
                    break;
                case "Октябрь":
                    date = new DateTime(2020, 10, 1);
                    break;
                case "Ноябрь":
                    date = new DateTime(2020, 11, 1);
                    break;
                case "Декабрь":
                    date = new DateTime(2020, 12, 1);
                    break;
                default:
                    date = new DateTime(2020, 2, 1);
                    break;
            }
            return date;
        }

       
    }
}
