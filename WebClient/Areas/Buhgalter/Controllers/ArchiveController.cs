using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using BusinessLogic.BindingModel;
using BusinessLogic.BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using WebClient.Areas.Buhgalter.Models;

namespace WebClient.Areas.Buhgalter.Controllers
{
    [Area("Buhgalter")]
    public class ArchiveController : Controller
    {
        private readonly IAgentLogic _agent;
        private readonly IDogovorLogic dogovorLogic;
        private readonly IZarplataLogic _zarplata;
        public ArchiveController(IDogovorLogic d, IZarplataLogic zarplata, IAgentLogic agent)
        {
            _agent = agent;
            dogovorLogic = d;
            _zarplata = zarplata;
        }
        public ActionResult Archive()
        {
            var name = _agent.Read(null);
            var zps = _zarplata.Read(null);
            var old = zps.Where(rec => (rec.data <= DateTime.Now.AddYears(-1))).Select(rec => (rec.Id, rec.Summa,
            (name.Where(r => r.Id == rec.UserId).Select(r => r.Name).FirstOrDefault()), rec.data));
            ViewBag.Zp = old;
            return View();
        }
        public ActionResult Archivation(ReportModel  model)
        {
            DateTime date1 = DateTime.Now;
            string fileName = model.puth + $"\\ArchiveOf{date1.Year}";
            Directory.CreateDirectory(fileName);
            if (Directory.Exists(fileName))
            {
                var zarplatas = _zarplata.Read(null);//все договоры
                bool proverca = false;
                var old = zarplatas.Where(rec => (rec.data <= DateTime.Now.AddYears(-1)));
                DataContractJsonSerializer jsonFormatter = new
               DataContractJsonSerializer(typeof(List<ZarplataViewModel>));
                using (FileStream fs = new FileStream(string.Format("{0}/{1}.json",
               fileName, "Zarplatas"), FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, old);
                }
                ZipFile.CreateFromDirectory(fileName, $"{fileName}.zip");
                Directory.Delete(fileName, true);
                Mail.SendMail(model.SendMail, $"{fileName}.zip", $"Archive");
                foreach (var delzp in old)
                    _zarplata.Delete(new ZarplataBindingModel { Id = delzp.Id});
                return RedirectToAction("Archive");
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(fileName);
                return RedirectToAction("Archive");
            }
        }
    }
}
