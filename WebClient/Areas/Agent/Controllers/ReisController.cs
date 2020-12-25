using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Areas.Agent.Models;

namespace WebClient.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class ReisController : Controller
    {
        private readonly IReisLogic _reis;
        private readonly IRaionLogic raions;
        private readonly IDogovorLogic _dogovor;
        public ReisController(IReisLogic reis, IRaionLogic raion, IDogovorLogic dogovor)
        {
            _reis = reis;
            raions = raion;
            _dogovor = dogovor;
        }
        public IActionResult Reis(int raion, SpisokModel model)
        {
            ViewBag.Reiss = _reis.Read(null);
            var Raion = raions.Read(null);
            ViewBag.Raions = Raion;
            Raion.Insert(0, new RaionViewModel { Name = "Все", Id = 0 });
            SpisokModel plvm = new SpisokModel
            {
                Raion = new SelectList(Raion, "Id", "Name")
            };
            var reisView = _reis.Read(null);
            List<ReisViewModel> reisV = new List<ReisViewModel>() { };
            ViewBag.Reiss = reisV;
            if (Validation(model) == true)
            {
                foreach (var i in reisView)
                {
                    if ((i.Cena > Convert.ToDouble(model.Cena1)) && (i.Cena < Convert.ToDouble(model.Cena2)) && (raion != 0))
                    {
                        if ((i.OfId == raion) || (i.ToId == raion) || (raion == 0))
                            reisV.Add(i);
                    }
                }
            }
            else
                ViewBag.Reiss = _reis.Read(null);
            return View(plvm);
        }
        public bool Validation(SpisokModel model)
        {
            try
            {
                if ((model.Cena1 != null) && (model.Cena2 == null))
                {
                    ModelState.AddModelError("Raion", "Нужно заполнить обе цены");
                    return false;
                }

                if ((model.Cena1 == null) && (model.Cena2 != null))
                {
                    ModelState.AddModelError("Raion", "Нужно заполнить обе цены");
                    return false;
                }
                if (model.Cena1 == null && model.Cena1 == null)
                    return false;
                if (Convert.ToDouble(model.Cena1) > Convert.ToDouble(model.Cena2))
                {
                    ModelState.AddModelError("Raion", "Цена 1 должна быть меньше");
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public IActionResult ChangeReis(int dogovorId)
        {
            ViewBag.DogovorId = dogovorId;
            var dogovor = _dogovor.Read(new DogovorBindingModel { Id = dogovorId})[0];
            if (dogovor.Dogovor_Reiss.Count == 0)
            {
                ViewBag.Reiss = _reis.Read(null);

            }

            else
            {
                foreach (var keyr in dogovor.Dogovor_Reiss)
                {
                    var ghjj = _reis.Read(new ReisBindingModel { Id = keyr.Value.Item2 });
                    ViewBag.Reiss = _reis.Read(new ReisBindingModel
                    {

                        OfId = (int)raions.Read(new RaionBindingModel { Id = _reis.Read(new ReisBindingModel { Id = keyr.Value.Item2 })[0].OfId})[0].Id
                    });
                }
                 



            }
     
            return View();
        }
        public IActionResult AddReis(int? reisId, int? dogovorId, int clientId, int drId)
        {
            ViewBag.ClientId = clientId;
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            var Cosmetic = _reis.Read(new ReisBindingModel
            {
                Id = reisId
            })?[0];
            if (Cosmetic == null)
            {
                return NotFound();
            }
            ViewBag.ReisName = Cosmetic.Name;
            ViewBag.DogovorId = dogovorId;
            if (drId!= 0)
            {
                ViewBag.DR = "1";
                var r = _dogovor.ReadReis(new Dogovor_ReisBM
                {
                    Id = drId
                })[0];
                return View(new Dogovor_ReisBM
                {
                    DogovorId = (int)dogovorId,
                    ReisId = (int)reisId,
                    Id= (int)r.Id,
                    Obem = r.Obem,
                    Comm = r.Comm,
                    ves=r.ves,
                    NadbavkaCena=r.NadbavkaCena,
                    NadbavkaTime=r.NadbavkaTime
                });

            }
            ViewBag.DR = "";
            return View(new Dogovor_ReisBM
            {Id=0,
                DogovorId = (int)dogovorId,
                ReisId = (int)reisId
            });

        }

    }
}
