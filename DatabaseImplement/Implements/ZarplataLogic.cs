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
                context.SaveChanges();
            }
        }
    }
}
