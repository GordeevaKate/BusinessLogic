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
        public ActionResult Archivation(ReportModel model)
        {
            DateTime date1 = DateTime.Now;
            string fileName = model.puth + $"\\ArchiveOf{date1.Year}";
            Directory.CreateDirectory(fileName);
            if (Directory.Exists(fileName))
            {
                var dogovors = dogovorLogic.Read(null);//все договоры
                var olddogovors = dogovorLogic.Read(new DogovorBindingModel { Id = 0 });//
                var oldreis = dogovorLogic.ReadReis(new Dogovor_ReisBM { Id = 0 });//устаревшие рейсы по договору
                bool proverca = false;
                
                DataContractJsonSerializer jsonFormatter = new
               DataContractJsonSerializer(typeof(List<DogovorViewModel>));
                using (FileStream fs = new FileStream(string.Format("{0}/{1}.json",
               fileName, "Dogovors"), FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, olddogovors);
                }
                jsonFormatter = new
             DataContractJsonSerializer(typeof(List<Dogovor_ReisVM>));
                using (FileStream fs = new FileStream(string.Format("{0}/{1}.json",
             fileName, "OldDogovorReis"), FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, oldreis);
                }
                ZipFile.CreateFromDirectory(fileName, $"{fileName}.zip");
                Directory.Delete(fileName, true);
                Mail.SendMail(model.SendMail, $"{fileName}.zip", $"Archive");
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
