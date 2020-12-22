
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;

namespace DatabaseImplement.Implements
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
