using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using DatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseImplement.Implements
{
  public  class ZarplataLogic: IZarplataLogic
    {
        public List<ZarplataViewModel> Rascet(int? AgentId, DateTime date)
        {
            using (var context = new KursachDatabase())
            {
                return context.Zarplatas
                 .Where(rec => rec.UserId == AgentId && rec.data >= date && rec.data <= date.AddMonths(1).AddDays(-1)
                      ).ToList()
               .Select(rec => new ZarplataViewModel
               {
                   Id = rec.Id,
                   UserId = rec.UserId,
                   Summa = rec.Summa,
                   data = rec.data
               })
                .ToList();
            }
        }
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
                element.Summa = model.Summa;
                element.Period = model.Period;
                context.SaveChanges();
            }
        }
    }
}
