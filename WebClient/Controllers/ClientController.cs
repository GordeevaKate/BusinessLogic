using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.HelperModels;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяBusinessLogic.Report;

namespace WebClient.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientLogic _client;
            private readonly IDogovorLogic _dogovor;
        public ClientController(IClientLogic client, IDogovorLogic dogovor)
        {
            _client = client;
            _dogovor = dogovor;
        }
        public bool Validation(int i)
        {
            if (i == 0)
            {
                ModelState.AddModelError("Passport", "Клиента не существует");
                ViewBag.Client = _client.Read(null);
                return false;
            }
            return true;
        }
        public IActionResult Client(ClientViewModel model)
        {
            bool t=true;
            if (model.DogovorId > 0) {
               var dogovor = _dogovor.Read(new DogovorBindingModel
                {
                    Id= model.DogovorId
                });
                 t =Validation(dogovor.Count);
                foreach (var r in dogovor)
                {
                    ViewBag.Client = _client.Read(new ClientBindingModel { Id = r.ClientId });
                }
                if (t != false)
                    return View();
            }
            if (model.Passport != null)
            {
                var client = _client.Read(new ClientBindingModel { Pasport=model.Passport});
                t = Validation(client.Count);
                ViewBag.Client = client;
                if (t != false)
                    return View();
            }
            ViewBag.Client = _client.Read(null);
            return View();
        }
        public IActionResult Dogovor(int? id)
        {
            ViewBag.Client = _client.Read(new ClientBindingModel { Id = (int)id }).FirstOrDefault();
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Dogovors = _dogovor.Read(new DogovorBindingModel
            {
                ClientId = (int)id,
                AgentId= (int)Program.Agent.Id
            });

            return View();
        }



        public IActionResult Report()
        {
            List<string> list = new List<string> { "Паспорт", "ФИО", "Номер телефона", "Email" };
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName =  "C:\\report-kursovaa\\Reportpdf.pdf",
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name}",
                Components = _client.Read(null)
            });
            return View();
        }

        public IActionResult CreateDogovor(int id=-1)
        {
           ViewBag.Itog = 0;
            ViewBag.ReisDogovor = _dogovor.Read(new DogovorBindingModel
            {
                ClientId = (int)id,
                AgentId = (int)Program.Agent.Id
            });
            return View();
        }
        public IActionResult ChangeDogovor(int? id)
        {
            ViewBag.Id = id;
            ViewBag.Itog = 0;
            ViewBag.ReisDogovor = _dogovor.Read(new DogovorBindingModel
            {
                Id=id
            });
            return View();
        }
    }
}
