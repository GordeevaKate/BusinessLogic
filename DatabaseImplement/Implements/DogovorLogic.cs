using DatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;

namespace DatabaseImplement.Implements
{
    public class DogovorLogic:IDogovorLogic

    {
        public void CreateOrUpdate(DogovorBindingModel model)
        {

            using (var context = new KursachDatabase())
            {
                Dogovor element = context.Dogovors.FirstOrDefault(rec =>  rec.Id != model.Id);

                if (model.Id.HasValue)
                {
                    element = context.Dogovors.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Склад не найден");
                    }
                }
                else
                {
                    element = new Dogovor();
                    context.Dogovors.Add(element);
                    element.ClientId = model.ClientId;
                    element.AgentId = model.AgentId;
                    element.data = model.data;
                   }
                element.Summa = model.Summa;
                context.SaveChanges();
            }
        }
             public void DeleteReisDogovor(Dogovor_ReisBM model)
        {
            using (var context = new KursachDatabase())
            {
                Dogovor_Reis element = context.Dogovor_Reiss.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Dogovor_Reiss.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<Dogovor_ReisVM> ReadReis(Dogovor_ReisBM model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Dogovor_Reiss
                 .Where(rec => model == null ||(rec.Id==model.Id) ||(rec.DogovorId==model.DogovorId && rec.ReisId==model.ReisId)
                      ).ToList()
               .Select(rec => new Dogovor_ReisVM
               {
                   Id = rec.Id,
                   DogovorId = rec.DogovorId,
                   ReisId = rec.ReisId,
                   Obem = rec.Obem,
                   ves = rec.ves,
                   Comm = rec.Comm,
                   NadbavkaCena = rec.NadbavkaCena,
                   NadbavkaTime = rec.NadbavkaTime

               })
                .ToList();
            }
        }
            public void AddReis(Dogovor_ReisBM model)
        {
            using (var context = new KursachDatabase())
            {
                var werehouseCosmetics = context.Dogovor_Reiss.FirstOrDefault(rec =>
                 rec.DogovorId == model.DogovorId && rec.ReisId == model.ReisId);
                if (werehouseCosmetics == null)
                {
                    context.Dogovor_Reiss.Add(new Dogovor_Reis
                    {
                        DogovorId = model.DogovorId,
                        ReisId = model.ReisId,
                        Obem=model.Obem,
                        ves=model.ves,
                        Comm=model.Comm,
                        NadbavkaCena=model.NadbavkaCena,
                        NadbavkaTime=model.NadbavkaTime
                    });
                }
                else
                {
                    werehouseCosmetics.Obem += model.Obem;
                    werehouseCosmetics.ves += model.ves;
                    werehouseCosmetics.Comm = model.Comm;
                    werehouseCosmetics.NadbavkaCena = model.NadbavkaTime;
                }

                Reis element = context.Reiss.FirstOrDefault(rec =>
                rec.Id == model.ReisId);
                context.SaveChanges();
            }
        }

        public void Delete(DogovorBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Dogovor element = context.Dogovors.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Dogovors.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<DogovorViewModel> Rascet(int? AgentId, DateTime date)
        {
            using (var context = new KursachDatabase())
            {
                return context.Dogovors
                 .Where(rec => rec.AgentId == AgentId && rec.data >= date && rec.data <= date.AddMonths(1).AddDays(-1)
                      ).ToList()
               .Select(rec => new DogovorViewModel
               {
                   Id = rec.Id,
                   ClientId = rec.ClientId,
                   Summa = rec.Summa,
                   data = rec.data,
                   Dogovor_Reiss = context.Dogovor_Reiss
                            .Include(recCC => recCC.Reiss)
                            .Where(recCC => recCC.DogovorId == rec.Id)
                            .ToDictionary(recCC => recCC.Id, recCC => ((int)recCC.DogovorId, recCC.ReisId, recCC.NadbavkaCena,recCC.NadbavkaTime, recCC.Comm, recCC.Obem, recCC.ves))
               })
                .ToList();
            }
        }
        public List<DogovorViewModel> Read(DogovorBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Dogovors
                 .Where(rec => model == null || (rec.Id == model.Id && model.AgentId == 0) || (rec.Id == model.Id && model.AgentId == rec.AgentId)
                   || (rec.ClientId == model.ClientId && rec.AgentId == model.AgentId)||(rec.data==model.data)
                      ).ToList()
               .Select(rec => new DogovorViewModel
               {
                   Id = rec.Id,
                   ClientId=rec.ClientId,
                   Summa = rec.Summa,
                   data = rec.data,
                   Dogovor_Reiss = context.Dogovor_Reiss
                            .Include(recCC => recCC.Reiss)
                            .Where(recCC => recCC.DogovorId == rec.Id)
                            .ToDictionary(recCC => recCC.Id, recCC => ((int)recCC.DogovorId, recCC.ReisId, recCC.NadbavkaCena,recCC.NadbavkaTime, recCC.Comm, recCC.Obem, recCC.ves))
               })
                .ToList();
            }
        }
    }
}
