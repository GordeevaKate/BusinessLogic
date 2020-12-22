using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Areas.Agent.Models;

namespace WebClient.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class ClientController : Controller
    {
        private readonly IReisLogic _reis;
        private readonly IClientLogic _client;
        private readonly IDogovorLogic _dogovor;
        public ClientController(IClientLogic client, IReisLogic reis, IDogovorLogic dogovor)
        {
            _reis = reis;
            _client = client;
            _dogovor = dogovor;
        }
        public IActionResult Client(SpisokClientViewModel model)
        {
            if (model.DogovorId > 0)
            {
                var dogovor = _dogovor.Read(new DogovorBindingModel
                {
                    Id = model.DogovorId
                });
                foreach (var r in dogovor)
                {
                    ViewBag.Client = _client.Read(new ClientBindingModel { Id = r.ClientId });
                }
                if (dogovor.Count==0)
                {
                    ModelState.AddModelError("Passport", "Клиента не существует");
                    ViewBag.Client = _client.Read(null);
                    return View();
                }
            }
            if (model.Passport != null)
            {
                var client = _client.Read(new ClientBindingModel { Pasport = model.Passport });
                ViewBag.Client = client;
                if (client.Count == 0)
                {
                    ModelState.AddModelError("Passport", "Клиента не существует");
                    ViewBag.Client = _client.Read(null);
                    return View();
                }
            }
            if (Validation(model.Obem) == true)
            {

                var clients = _client.Read(null);
                List<ClientViewModel> listclient = new List<ClientViewModel> { };
                foreach (var client in clients)
                {
                    double obem = 0;
                    var dogovorss = _dogovor.Read(null);
                    var dogovors = _dogovor.Read(new DogovorBindingModel
                    {
                        ClientId = client.Id,
                        AgentId = (int)Program.Agent.Id

                    });
                    foreach (var dogovor in dogovors)
                    {
                        foreach (var reis in dogovor.Dogovor_Reiss)
                        {
                            obem += reis.Value.Item5;
                        }
                    }
                    if (obem >= Convert.ToDouble(model.Obem))
                    {
                        listclient.Add(client);
                    }
                }
                ViewBag.Client = listclient;
                return View();
            }
            else {
                    if (model.Obem != "0")
                    ModelState.AddModelError("Obem", "Объем введен неправильно");
                }
            ViewBag.Client = _client.Read(null);
            return View();
        }
        public bool Validation( string Obem)
        {
            if( (Obem != null)&&(Obem!="0"))
            {
                return double.TryParse(Obem, out double t);
            }
            return true;
        }
        public IActionResult Report()//кнопка отчет на странице клиент
        {
            List<string> list = new List<string> { "Паспорт", "ФИО", "Номер телефона", "Email" };
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = "C:\\report-kursovaa\\Reportpdf.pdf",
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name}",
                Components = _client.Read(null)
            });
            return RedirectToAction("Client");
        }



    }
}
