using BusinessLogic;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Unity;

namespace WebClient.Areas.Agent.Controllers
{
    [Area("Agent")]
    public class ArchiveController : Controller
    {

        private readonly IDogovorLogic dogovorLogic;
        private readonly IReisLogic rlogic;
        public ArchiveController(IDogovorLogic d, IReisLogic r)
        {
            dogovorLogic=d;
            rlogic = r;
        }
        public ActionResult Archive()
        {
           Dictionary<int,DateTime> dats = new Dictionary<int, DateTime> { };
            var dogovors = dogovorLogic.Read(null);//все договоры
            var olddogovors = dogovorLogic.Read(new DogovorBindingModel { Id = 0 });//
            var oldreis = dogovorLogic.ReadReis(new Dogovor_ReisBM { Id = 0 });//устаревшие рейсы по договору
            bool proverca = false;

            foreach (var dogovor in dogovors)
            {
                dats.Add((int)dogovor.Id, new DateTime());
                proverca = false;
                foreach (var dr in dogovor.Dogovor_Reiss)
                {
                    var reis1 = rlogic.Read(new ReisBindingModel { Id = dr.Value.Item2 })[0];
                    DateTime dt1 = dogovor.data.AddDays(reis1.Time + dr.Value.Item4);
                    if (dt1 > dats[(int)dogovor.Id])
                    {
                        dats[(int)dogovor.Id] = dt1;
                    }
                    DateTime dt2 = DateTime.Now;
                    if (dt1 > dt2)
                    {
                        proverca = true;
  
                    }
                }
                if (proverca != true)
                {
                    olddogovors.Add(dogovor);
                    foreach (var dr in dogovor.Dogovor_Reiss)
                    {

                        oldreis.Add(dogovorLogic.ReadReis(new Dogovor_ReisBM { Id = dr.Key })[0]);
                    }
                }
            }
            ViewBag.Dats = dats;
            ViewBag.dogovors = olddogovors;

            return View();
        }
        public ActionResult Archivation()
        {
            DateTime date1 = DateTime.Now;
            string fileName = "C:\\report-kursovaa\\Archive";
            Directory.CreateDirectory($@"{fileName}\\ArchiveOf{date1.Year}");
            fileName = $@"{fileName}\\ArchiveOf{date1.Year}";
            if (Directory.Exists(fileName))
            {
                var dogovors = dogovorLogic.Read(null);//все договоры
                var olddogovors = dogovorLogic.Read(new DogovorBindingModel { Id = 0 });//
                var oldreis = dogovorLogic.ReadReis(new Dogovor_ReisBM { Id = 0 });//устаревшие рейсы по договору
                bool proverca=false;
                foreach (var dogovor in dogovors)
                {
                    proverca = false;
                       foreach(var dr in dogovor.Dogovor_Reiss)
                        {
                            var reis1=  rlogic.Read(new ReisBindingModel { Id = dr.Value.Item2 })[0];
                        DateTime dt1 = dogovor.data.AddDays(reis1.Time + dr.Value.Item4);
                        DateTime dt2 = DateTime.Now;
                        if (dt1> dt2)
                            {
                                proverca = true;
                            }
                       }
                        if (proverca != true)
                    {
                        olddogovors.Add(dogovor);
                        foreach (var dr in dogovor.Dogovor_Reiss)
                        {
                            oldreis.Add(dogovorLogic.ReadReis(new Dogovor_ReisBM { Id = dr.Key })[0]);
                       
                        }
                        dogovorLogic.Delete(new DogovorBindingModel { Id = dogovor.Id });
                    }
                }
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
