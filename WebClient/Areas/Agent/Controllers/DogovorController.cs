using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                        if (reis_dogovor.Value.Item6 / reis_dogovor.Value.Item5 > 250)
                        {
                            Itog = Itog + reis_dogovor.Value.Item3 + reis[0].Cena * reis_dogovor.Value.Item6;

                        }
                        else
                            Itog = Itog + reis_dogovor.Value.Item3 + reis[0].Cena * reis_dogovor.Value.Item5;
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

            if (ModelState.IsValid)
            {
                try
                {
                    _dogovor.AddReis(model);
                }
                catch (Exception exception)
                {
                    TempData["ErrorLackInWerehouse"] = exception.Message;
                    return RedirectToAction("AddReis", "Reis", new
                    {
                        reisID = model.ReisId,
                        dogovorId = model.DogovorId
                    });
                }
            }
            return RedirectToAction("ChangeDogovor", new { id = model.DogovorId });
        }

        public IActionResult Dogovor(int? id)
        {
            Program.ClientId = (int)id;
            ViewBag.ClientId = id;
            ViewBag.Client = _client.Read(new ClientBindingModel { Id = (int)id }).FirstOrDefault();
            if (id == null)
            {
                return NotFound();
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
