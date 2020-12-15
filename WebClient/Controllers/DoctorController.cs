using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HospitalBusinessLogic.Interfaces;


namespace HospitalWebClient.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorLogic _doctor;
        public DoctorController(IDoctorLogic doctor)
        {
            _doctor = doctor;
        }
        public IActionResult Doctor()
        {
            ViewBag.Doctors = _doctor.Read(null);
            return View();
        }
    }
}