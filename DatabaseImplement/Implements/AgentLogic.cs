using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;

namespace DatabaseImplement.Implements
{
    public class AgentLogic : IAgentLogic
    {
        public List<AgentViewModel> Read(AgentBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Agents
                 .Where(rec => model == null
                   || (rec.UserId == model.UserId) || (rec.Name == model.Name) || (rec.Id == model.Id))
               .Select(rec => new AgentViewModel
               {
                   Id = rec.Id,
                   Name = rec.Name,
                   Oklad = rec.Oklad,
                   Comission = rec.Comission,
            })
                .ToList();
            }
        }
    }
}
