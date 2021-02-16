using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using DatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseImplement.Implements
{
  public  class ZarplataLogic: IZarplataLogic
    {
        public void CreateOrUpdate(ZarplataBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Zarplata element = model.Id.HasValue ? null : new Zarplata();
                if (model.Id.HasValue)
                {
                    element = context.Zarplatas.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Zarplata();
                    context.Zarplatas.Add(element);
                }
                element.UserId = model.UserId;
                element.Name = model.Name;
                element.Summa = model.Summa;
                element.data = model.data;
                context.SaveChanges();
            }
        }
        public List<ZarplataViewModel> Read(ZarplataBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Zarplatas
                 .Where(rec => model == null || (rec.Id == model.Id) || (rec.data == model.data) 
                 || (rec.UserId == model.UserId)
                 ).ToList()
               .Select(rec => new ZarplataViewModel
               {
                   Id = rec.Id,
                   Summa = rec.Summa,
                   Name = rec.Name,
                   data = rec.data,
                   UserId = rec.UserId
               })
                .ToList();
            }
        }
        public void Delete(ZarplataBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Zarplata element = context.Zarplatas.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Zarplatas.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
    }
}
