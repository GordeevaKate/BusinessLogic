using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.BindingModel;
using BusinessLogic.HelperModels;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Areas.Buhgalter.Models;

namespace WebClient.Areas.Buhgalter.Controllers
{
    [Area("Buhgalter")]
    public class DogovorController : Controller
    {
        private readonly IDogovorLogic _dogovor;
        private readonly IClientLogic _client;
        private readonly IReisLogic _reis;
        private readonly IRaionLogic _raion;
        public DogovorController(IClientLogic client, IRaionLogic raion, IReisLogic reis, IDogovorLogic dogovor)
        {
            _client = client;
            _dogovor = dogovor;
            _reis = reis;
            _raion = raion;
        }
        [HttpGet]
        public IActionResult Report(ReportModel model, int dogovorid)
        {
            List<string> list = new List<string> { "Название", "Цена", "Откуда", "Куда", "Время выполнения", "Объем товара", "Вес товара" };
            var clientsall = _client.Read(null);
            var clients = _client.Read(new ClientBindingModel { Id = 0 });
            foreach (var client in clientsall)
            {
                var dogovorofclient = _dogovor.Read(new DogovorBindingModel { ClientId = client.Id, AgentId = (int)Program.Agent.Id });
                if (dogovorofclient.Count >= 0)
                {
                    clients.Add(client);
                }
            }
            SaveToPdf.CreateDocDogovor(new Info
            {
                FileName = model.puth + $"ReportDogovorpdf{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf",
                Colon = list,
                Title = $" Договор {dogovorid}",
                Client = _client.Read(new ClientBindingModel { Id = Program.ClientId })[0].ClientFIO,
                Agent = Program.Agent.Name,
                dogovor = _dogovor.Read(new DogovorBindingModel { Id = dogovorid })[0],
                dogovor_Reis = _dogovor.ReadReis(null),
                raion = _raion.Read(null),
                reiss = _reis.Read(null)
            });
            return RedirectToAction("Dogovor", new { id = Program.ClientId });
        }
        public IActionResult ChangeDogovor(int? id)
        {
            ViewBag.Raions = _raion.Read(null);
            if (id != null)
            {
                ViewBag.Id = id;
                ViewBag.ClientId = Program.ClientId;
                ViewBag.Reis = _reis.Read(null);
                var Dogovor = _dogovor.Read(new DogovorBindingModel
                {
                    Id = id
                });
                ViewBag.Data = Dogovor[0].data;
                double Itog = 0;
                foreach (var dogovor in Dogovor)
                    foreach (var reis_dogovor in dogovor.Dogovor_Reiss)
                    {
                        var reis = _reis.Read(new ReisBindingModel { Id = reis_dogovor.Value.Item2 });
                        if (reis_dogovor.Value.Item7 / reis_dogovor.Value.Item6 > 250)
                        {
                            Itog = Itog + reis_dogovor.Value.Item3 + reis[0].Cena * reis_dogovor.Value.Item7;

                        }
                        else
                            Itog = Itog + reis_dogovor.Value.Item3 + reis[0].Cena * reis_dogovor.Value.Item6;
                    }
                ViewBag.Itog = Itog;
                ViewBag.ReisDogovor = _dogovor.Read(new DogovorBindingModel
                {
                    Id = id
                });
                _dogovor.CreateOrUpdate(new DogovorBindingModel { Id = id, data = DateTime.Now, Summa = Itog, ClientId = Program.ClientId, AgentId = (int)Program.Agent.Id });
                return View();
            }
            else
            {
                DateTime datetime = DateTime.Now;
                _dogovor.CreateOrUpdate(new DogovorBindingModel { data = datetime, Summa = 0, ClientId = Program.ClientId, AgentId = (int)Program.Agent.Id });
                var Dogovor = _dogovor.Read(new DogovorBindingModel
                {
                    data = datetime,
                    AgentId = (int)Program.Agent.Id,
                    ClientId = Program.ClientId
                });
                return RedirectToAction("ChangeReis", "Reis", new { dogovorId = Dogovor[Dogovor.Count - 1].Id });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public bool Validation([Bind("DogovorId, ReisId,Id, Comm, NadbavkaCena, NadbavkaTime, Obem, ves")] Dogovor_ReisBM model)
        {

            var reis = _reis.Read(new ReisBindingModel { Id = model.ReisId })[0];

            var ts = _dogovor.ReadReis(new Dogovor_ReisBM { DogovorId = model.DogovorId, ReisId = model.ReisId });
            if (model.Comm == null || model.Comm == "")
            {
                model.Comm = "  ";
            }
            if (_dogovor.ReadReis(new Dogovor_ReisBM { DogovorId = model.DogovorId, ReisId = model.ReisId }).Count != 0)
            {
                var t = ts[0];
                if (model.Id > 0)
                {

                    if (t.ves / t.Obem > 250)
                    {
                        if (reis.Cena * t.ves + model.NadbavkaCena <= 0)
                        {
                            TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                            return false;

                        }
                    }
                    else
                    {
                        if (reis.Cena * t.Obem + model.NadbavkaCena <= 0)
                        {
                            TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                            return false;
                        }
                    }

                    if (reis.Time + model.NadbavkaTime <= 0)
                    {
                        TempData["ErrorLack"] = "Time за перевозку не может быть меньше или равна 0";
                        return false;

                    }
                }
                else
                {
                    if ((t.ves + model.ves) / (t.Obem + t.Obem) > 250)
                    {
                        if (reis.Cena * (t.ves + model.ves) + model.NadbavkaCena + t.NadbavkaCena <= 0)
                        {
                            TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                            return false;

                        }
                    }
                    else
                    {
                        if (reis.Cena * t.Obem * model.Obem + t.NadbavkaCena + model.NadbavkaCena <= 0)
                        {
                            TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                            return false;
                        }
                    }

                    if (reis.Time + model.NadbavkaTime + t.NadbavkaTime <= 0)
                    {
                        TempData["ErrorLack"] = "Time за перевозку не может быть меньше или равна 0";
                        return false;

                    }
                }



            }
            else
            {

                if (model.ves / model.Obem > 250)
                {
                    if (reis.Cena * model.ves + model.NadbavkaCena <= 0)
                    {
                        TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                        return false;

                    }
                }
                else
                {
                    if (reis.Cena * model.Obem + model.NadbavkaCena <= 0)
                    {
                        TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                        return false;
                    }
                }
                if (reis.Time + model.NadbavkaTime <= 0)
                {
                    TempData["ErrorLack"] = "Time за перевозку не может быть меньше или равна 0";
                    return false;

                }
            }
            _dogovor.AddReis(model);
            return true;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReis([Bind("DogovorId, ReisId,Id, Comm, NadbavkaCena, NadbavkaTime, Obem, ves")] Dogovor_ReisBM model)
        {
            if (Validation(model))
            {
                return RedirectToAction("ChangeDogovor", new { id = model.DogovorId });
            }
            else
            {
                TempData["ErrorLack"] += ". Не получилось добавить";
                return RedirectToAction("AddReis", "Reis", new
                {
                    reisID = model.ReisId,
                    dogovorId = model.DogovorId,
                    drId = model.Id
                });
            }
        }
        public IActionResult Dogovor(int? id, AgentListModel model)
        {
            Program.ClientId = (int)id;
            ViewBag.ClientId = id;
            ViewBag.Client = _client.Read(new ClientBindingModel { Id = (int)id }).FirstOrDefault();
            if (id == null)
            {
                return NotFound();
            }
            var dogovor = _dogovor.Read(null);
            foreach (var d in dogovor)
            {
                if (d.Dogovor_Reiss.Count == 0)
                {
                    _dogovor.Delete(new DogovorBindingModel { Id = d.Id });
                }
            }
            if (model.Id > 0)
            {
                dogovor = _dogovor.Read(new DogovorBindingModel
                {
                    Id = model.Id,
                    ClientId = (int)id
                });
                foreach (var r in dogovor)
                {

                    ViewBag.Dogovors = _client.Read(new ClientBindingModel { Id = r.ClientId });
                }
                if (dogovor.Count != 0)
                {
                    ViewBag.Dogovors = dogovor;
                    return View();
                }
                ModelState.AddModelError("Passport", "Такого Договора у клиента не существует");
            }
            ViewBag.Dogovors = _dogovor.Read(new DogovorBindingModel
            {
                ClientId = (int)id,
                AgentId = (int)Program.Agent.Id
            });

            return View();
        }
    }
}
