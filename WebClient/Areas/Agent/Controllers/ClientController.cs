﻿using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebClient.Areas.Agent.Models;
using BusinessLogic.BusinessLogic;
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
        public IActionResult Client(ClientSpisokViewModel model)
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
                    return View();
                }
                if (dogovor.Count==0)
                {
                    ModelState.AddModelError("Passport", "Договора не существует");
                    ViewBag.Client = _client.Read(null);
                    return View();
                }
            }
            if (model.Passport != null)
            {
                if (!Regex.IsMatch(model.Passport, @"^[1-9]{10}$"))
                {
                    ModelState.AddModelError("Passport", "Паспорт введен некорректно");
                    ViewBag.Client = _client.Read(null);
                    return View();
                }
                var client = _client.Read(new ClientBindingModel { Pasport = model.Passport });
                ViewBag.Client = client;
                if (client.Count == 0)
                {
                    ModelState.AddModelError("Passport", "Клиента не существует");
                    ViewBag.Client = _client.Read(null);
                    return View();
                }
                return View();
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
                            obem += reis.Value.Item6;
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
        public IActionResult ReadOfReportSpisok(ReportViewModel model)//кнопка отчет на странице клиент
        {
            string filename = model.puth+$"ReportClientpdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.";
            List<string> list = new List<string> { "Паспорт", "ФИО", "Номер телефона", "Email" };
            var clientsall = _client.Read(null);
            var clients = _client.Read(new ClientBindingModel { Id = 0 });
            foreach (var client in clientsall)
            {
               var dogovorofclient= _dogovor.Read(new DogovorBindingModel { ClientId = client.Id, AgentId = (int)Program.Agent.Id });
                if (dogovorofclient.Count >= 0)
                {
                    clients.Add(client);
                }
            }
            SaveToPdf.CreateDoc(new Info
            {
                FileName =filename+"pdf",
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name}",
                Clients = clients
            });
                Mail.SendMail(model.SendMail, $"{filename}pdf", $"Список клиентов для Агента{Program.Agent.Name}");
            SaveToExcel.CreateDoc(new Info { 
         FileName =filename+ "xlsx",
                Colon = list,
                Title = $" Список клиентов для Агента{Program.Agent.Name}",
                Clients = clients
            }


        ); 

                Mail.SendMail(model.SendMail, $"{filename}xlsx", $"Список клиентов для Агента{Program.Agent.Name} за {DateTime.Now}");

            return RedirectToAction("Client");
        }
        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public ViewResult AddClient(AddClientModel client)
        {
            if (String.IsNullOrEmpty(client.Pasport))
            {
                ModelState.AddModelError("", "Введите номер паспорта");
                return View(client);
            }
            if (!Regex.IsMatch(client.Pasport, @"^[1-9]{10}$"))
            {
                ModelState.AddModelError("", "Паспорт введен некорректно");
                return View(client);
            }
            if (_client.Read(new ClientBindingModel { Pasport = client.Pasport }).Count != 0)
            {
                ModelState.AddModelError("", "Такой паспорт уже существует");
                return View(client);
            }
            if (client.Pasport.Length != 10)
            {
                ModelState.AddModelError("", "Паспорт состоит из 10 цифр");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.Email))
            {
                ModelState.AddModelError("", "Введите Email");
                return View(client);
            }
            if (!Regex.IsMatch(client.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                ModelState.AddModelError("", "Email введен некорректно");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.ClientFIO))
            {
                ModelState.AddModelError("", "Введите ФИО");
                return View(client);
            }
            if (String.IsNullOrEmpty(client.PhoneNumber))
            {
                ModelState.AddModelError("", "Введите номер телефона");
                return View(client);
            }
            if (!Regex.IsMatch(client.PhoneNumber, @"^[1-9]{1}[0-9]{5,10}"))
            {
                ModelState.AddModelError("", "Заполните телефон правильно: 89176258099 или 565656");
                return View(client);

            }
            _client.CreateOrUpdate(new ClientBindingModel
            {
                ClientFIO = client.ClientFIO,
                Pasport = client.Pasport,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                UserId = 0
            });
            ModelState.AddModelError("", "Вы успешно зарегистрированы");
            return View("AddClient", client);
        }


    }
}
