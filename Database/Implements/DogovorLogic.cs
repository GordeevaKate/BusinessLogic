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
                 .Where(rec => model == null
                   || (rec.ClientId == model.ClientId && rec.AgentId == model.AgentId)
                      )
               .Select(rec => new DogovorViewModel
               {
                   Id = rec.Id,
                   Summa = rec.Summa,
                   data = rec.data
    })
                .ToList();
            }
        }
    }
}
