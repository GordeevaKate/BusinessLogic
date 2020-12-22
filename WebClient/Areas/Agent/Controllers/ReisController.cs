using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class ReisController : Controller
    {
        private readonly IReisLogic _reis;
        private readonly IRaionLogic raions;
        public ReisController(IReisLogic reis, IRaionLogic raion)
        {
            _reis = reis;
            raions = raion;
        }

        public ActionResult Delete(int id)
        {
            _reis.Delete(new ReisBindingModel { Id = id });
            return RedirectToAction("Reis");
        }


        public IActionResult Reis(int raion, SpisokModel model)
        {

            ViewBag.Reiss = _reis.Read(null);
            var Raion = raions.Read(null);
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
            ViewBag.Reiss = _reis.Read(null);
            return View();
        }
        public IActionResult AddReis(int? reisId, int? dogovorId, int clientId)
        {
            ViewBag.ClientId = clientId;
            if (TempData["ErrorLackInWerehouse"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLackInWerehouse"].ToString());
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
            return View(new Dogovor_ReisBM
            {
                DogovorId = (int)dogovorId,
                ReisId = (int)reisId,
            });
        }

    }
}
