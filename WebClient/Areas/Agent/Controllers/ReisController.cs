using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Agent.Controllers
{
    public class ReisController : Controller
    {
        // GET: ReisController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReisController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
