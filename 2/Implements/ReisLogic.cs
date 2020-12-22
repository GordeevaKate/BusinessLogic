
using DatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;

namespace DatabaseImplement.Implements
{
    public class ReisLogic:IReisLogic
    {
        public void CreateOrUpdate(ReisBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Reis element = model.Id.HasValue ? null : new Reis();
                if (model.Id.HasValue)
                {
                    element = context.Reiss.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Reis();
                    context.Reiss.Add(element);
                }
                element.Name = model.Name;
                element.Cena = model.Cena;
                element.OfId = model.OfId;
                element.ToId = model.ToId;
                element.Time = model.Time;
                context.SaveChanges();
            }
        }
        public void Delete(ReisBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Reis element = context.Reiss.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Reiss.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ReisViewModel> Read(ReisBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Reiss
                 .Where(rec => model == null
                   || rec.Id == model.Id
                      )
               .Select(rec => new ReisViewModel
               {
                   Id = rec.Id,
                   OfId=rec.OfId,
                   Name=rec.Name,
                   ToId=rec.ToId,
                   Cena=rec.Cena,
                   Time=rec.Time
               })
                .ToList();
            }
        }
    }
}
