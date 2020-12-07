
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяBusinessLogic.ViewModel;

namespace Database.Implements
{
    public class RaionLogic: IRaionLogic
    {
        public List<RaionViewModel> Read(RaionBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Raions
                 .Where(rec => model == null
                   || rec.Id == model.Id||rec.Name==model.Name
                  )
               .Select(rec => new RaionViewModel
               {
                   Id = rec.Id,
                   Name = rec.Name,
               })
                .ToList();
            }
        }
    }
}
