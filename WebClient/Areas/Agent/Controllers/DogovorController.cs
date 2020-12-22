﻿using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
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
    public class DogovorController : Controller
    {
        private readonly IDogovorLogic _dogovor;
        private readonly IClientLogic _client;
        private readonly IReisLogic _reis;
        public DogovorController(IClientLogic client, IReisLogic reis, IDogovorLogic dogovor)
        {
            _client = client;
            _dogovor = dogovor;
            _reis = reis;
        }
        public ActionResult Delete(int id, int dogovotId)
        {
            _dogovor.DeleteReisDogovor(new Dogovor_ReisBM { Id = id });
            return RedirectToAction("ChangeDogovor", new { id = dogovotId });
        }
        public IActionResult ChangeDogovor(int? id)
        {

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
        public IActionResult AddReis([Bind("DogovorId, ReisId, Comm, Nadbavka", "Obem", "ves")] Dogovor_ReisBM model)
        {
            if (_dogovor.ReadReis(new Dogovor_ReisBM { DogovorId = model.DogovorId, ReisId = model.ReisId }).Count != 0)
            {
                var reis = _reis.Read(new ReisBindingModel { Id = model.ReisId })[0];
                if (model.ves / model.Obem > 250)
                {
                    if (reis.Cena * model.ves +model.NadbavkaCena<=0)
                    {
                        TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                        return RedirectToAction("AddReis", "Reis", new
                        {
                            reisID = model.ReisId,
                            dogovorId = model.DogovorId
                        });

                    }
                }
                else
                {
                    if (reis.Cena * model.Obem + model.NadbavkaCena<=0)
                    {
                        TempData["ErrorLack"] = "Цена за перевозку не может быть меньше или равна 0";
                        return RedirectToAction("AddReis", "Reis", new
                        {
                            reisID = model.ReisId,
                            dogovorId = model.DogovorId
                        });
                    }
                }

            }
            else
            {
                if (model.Obem <= 0)
                {
                    TempData["ErrorLack"] = "Объем не должен быть меньше или равна нулю";
                    return RedirectToAction("AddReis", "Reis", new
                    {
                        reisID = model.ReisId,
                        dogovorId = model.DogovorId
                    });
                }
                if (model.ves <= 0)
                {
                    TempData["ErrorLack"] = "Вес не должен быть меньше или равна нулю";
                    return RedirectToAction("AddReis", "Reis", new
                    {
                        reisID = model.ReisId,
                        dogovorId = model.DogovorId
                    });
                }
            }
             _dogovor.AddReis(model);
            return RedirectToAction("ChangeDogovor", new { id = model.DogovorId });
        }
        public IActionResult Dogovor(int? id, SpisokClientViewModel model)
        {
            Program.ClientId = (int)id;
            ViewBag.ClientId = id;
            ViewBag.Client = _client.Read(new ClientBindingModel { Id = (int)id }).FirstOrDefault();
            if (id == null)
            {
                return NotFound();
            }
            if (model.DogovorId > 0)
            {
                var dogovor = _dogovor.Read(new DogovorBindingModel
                {
                    Id = model.DogovorId,
                    ClientId= (int)id
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