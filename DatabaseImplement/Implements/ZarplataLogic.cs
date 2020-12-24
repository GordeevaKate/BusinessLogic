using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
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
    }
}
