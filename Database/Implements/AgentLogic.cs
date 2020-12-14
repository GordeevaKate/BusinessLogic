using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяBusinessLogic.ViewModel;

namespace Database.Implements
{
    public class AgentLogic : IAgentLogic
    {
        public void CreateOrUpdate(AgentBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(AgentBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<AgentViewModel> Read(AgentBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Agents
                 .Where(rec => model == null
                   || (rec.UserId == model.UserId ))
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
