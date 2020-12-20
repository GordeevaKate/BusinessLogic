using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяПис.BindingModel;

namespace WebClient.Controllers
{
    public class DogovorController : Controller
    {
        private readonly IDogovorLogic _dogovor;
        private readonly IClientLogic _client;
        public DogovorController( IClientLogic client, IDogovorLogic dogovor)
        {
            _client = client;
            _dogovor = dogovor;
        }
        public IActionResult ChangeDogovor(int? id)
        {
         
            ViewBag.Id = id;

            ViewBag.Itog = 0;
            ViewBag.ReisDogovor = _dogovor.Read(new DogovorBindingModel
            {
                Id = id
            });
            return View();
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
        public IActionResult CreateDogovor(int id = -1)//страницы создание договоров
        {
            ViewBag.Itog = 0;
            ViewBag.ReisDogovor = _dogovor.Read(new DogovorBindingModel
            {
                ClientId = (int)id,
                AgentId = (int)Program.Agent.Id
            });
            return View();
        }

        public IActionResult Dogovor(int? id, int? clientid)
        {
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
