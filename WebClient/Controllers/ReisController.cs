﻿using Database.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяBusinessLogic.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using КурсоваяBusinessLogic.BindingModel;
using System.IO;
using HospitalWebClient.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WebClient.Controllers
{
    public class ReisController: Controller
    {
        private readonly IReisLogic _doctor;
        private readonly IRaionLogic raions;
        public ReisController(IReisLogic reis, IRaionLogic raion)
        {
            _doctor = reis;
            raions = raion;
        }

        public ActionResult Delete(int id)
        {
            _doctor.Delete(new ReisBindingModel { Id = id });
            return RedirectToAction("Reis");
        }


        public IActionResult Reis( int raion, SpisokModel model)
        {

            ViewBag.Reiss = _doctor.Read(null);
            var Raion = raions.Read(null);
            Raion.Insert(0, new RaionViewModel { Name = "Все", Id = 0 });
            SpisokModel plvm = new SpisokModel
            {
                Raion = new SelectList(Raion, "Id", "Name")
            };
            var reisView = _doctor.Read(null);
            List<ReisViewModel> reisV = new List<ReisViewModel>() { };
            ViewBag.Reiss = reisV;
            if(Validation(model)==true)
            { 
                foreach (var i in reisView)
                {
                    if ((i.Cena > Convert.ToDouble(model.Cena1)) && (i.Cena < Convert.ToDouble(model.Cena2)) && (raion != 0))
                    {
                        if((i.OfId== raion) ||(i.ToId== raion)||(raion==0))
                        reisV.Add(i);
                    }
                       
                }
            }
            else
                ViewBag.Reiss = _doctor.Read(null);
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
                if (model.Cena1==null&&model.Cena1 == null)
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

        public IActionResult Report()
        {
            string fileName = "C:\\report-kursovaa\\Reportpdf.pdf";

            return RedirectToAction("Reis");
        }


        public ActionResult Diagram()
        {
            return RedirectToAction("Reis");
        }
    }
}
