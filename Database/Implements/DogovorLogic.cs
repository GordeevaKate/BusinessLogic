using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяBusinessLogic.ViewModel;

namespace Database.Implements
{
    public class DogovorLogic:IDogovorLogic

    {
        public void CreateOrUpdate(DogovorBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(DogovorBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<DogovorViewModel> Read(DogovorBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Dogovors
                 .Where(rec => model == null||( rec.Id==model.Id && model.AgentId == 0)||(rec.Id==model.Id && model.AgentId == rec.AgentId)
                   || (rec.ClientId == model.ClientId && rec.AgentId == model.AgentId)
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
                            .ToDictionary(recCC => recCC.DogovorId, recCC => ((int)recCC.Id, recCC.ReisId, recCC.Nadbavka, recCC.Comm, recCC.Obem, recCC.ves))
               })
                .ToList();
            }
        }
    }
}
