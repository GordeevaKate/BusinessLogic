using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using DatabaseImplement.Models;

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
        public void CreateOrUpdate(AgentBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Agent element = model.Id.HasValue ? null : new Agent();
                if (model.Id.HasValue)
                {
                    element = context.Agents.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Agent();
                    context.Agents.Add(element);
                }
                element.Name = model.Name;
                element.Oklad = model.Oklad;
                element.UserId = model.UserId;
                element.Comission = model.Comission;
                context.SaveChanges();
            }
        }
    }
}
