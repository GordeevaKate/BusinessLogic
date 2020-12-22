using BusinessLogic.BindingModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebClient.Models;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;

namespace WebClient.Controllers
{
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
        public bool Validation(int i){//проверка введеных данных на странице клиент
            if (i == 0)
            {
                ModelState.AddModelError("Passport", "Клиента не существует");
                ViewBag.Client = _client.Read(null);
                return false;
            }
            return true;
        }
      


        public IActionResult Report()//кнопка отчет на странице клиент
        {
            List<string> list = new List<string> { "Паспорт", "ФИО", "Номер телефона", "Email" };
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName =  "C:\\report-kursovaa\\Reportpdf.pdf",
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name}",
                Components = _client.Read(null)
            });
            return RedirectToAction("Client");
        }

   
       
    }
}
