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
        private readonly IAgentLogic _agent;
        private readonly IReisLogic _reis;
        private readonly IRaionLogic _raion;
        public DogovorController(IAgentLogic agent, IRaionLogic raion, IReisLogic reis, IDogovorLogic dogovor)
        {
            _agent = agent;
            _dogovor = dogovor;
            _reis = reis;
            _raion = raion;
        }
        [HttpGet]
        public IActionResult Dogovor(string? FIO, AgentListModel model)
        {
            ViewBag.Agent = FIO;

            var id = _agent.Read(new AgentBindingModel { Name = FIO }).FirstOrDefault();
            if (FIO == null)
            {
                return NotFound();
            }
            var dogovor = _dogovor.Read(null);
            /*foreach (var d in dogovor)
            {
                if (d.Dogovor_Reiss.Count == 0)
                {
                    _dogovor.Delete(new DogovorBindingModel { Id = d.Id });
                }
            }*/
            /*if (model.Id > 0)
            {
                dogovor = _dogovor.Read(new DogovorBindingModel
                {
                    AgentId = (int)model.Id,
                    
                });
                foreach (var r in dogovor)
                {

                    ViewBag.Dogovors = _dogovor.Read(new DogovorBindingModel { AgentId = (int)id.Id });
                }
                if (dogovor.Count != 0)
                {
                    ViewBag.Dogovors = dogovor;
                    return View();
                }
                ModelState.AddModelError("Passport", "Такого Договора у клиента не существует");
            }*/
            ViewBag.Dogovors = _dogovor.Read(new DogovorBindingModel
            {
               
                AgentId = (int)id.Id
            });

            return View();
        }
    }
}
