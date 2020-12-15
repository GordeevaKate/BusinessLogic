using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HospitalBusinessLogic.BindingModel;
using HospitalBusinessLogic.BusinessLogic;

namespace HospitalWebClient.Controllers
{
    public class BackUpController : Controller
    {
        private readonly BackUpAbstractLogic _backUp;
        public BackUpController(BackUpAbstractLogic backUp)
        {
            _backUp = backUp;
        }
        public IActionResult BackUp()
        {
            return View();
        }
        public IActionResult BackUpToJson()
        {
            string fileName = "E:\\data\\бэкап";
            if (Directory.Exists(fileName))
            {
                _backUp.CreateArchive(fileName);
                return RedirectToAction("BackUp");
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(fileName);
                _backUp.CreateArchive(fileName);
                return RedirectToAction("BackUp");
            }
        }
    }
}