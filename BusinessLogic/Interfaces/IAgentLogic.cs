using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{

        public interface IAgentLogic
        {
            List<AgentViewModel> Read(AgentBindingModel model);
            void CreateOrUpdate(AgentBindingModel model);
        }
}
