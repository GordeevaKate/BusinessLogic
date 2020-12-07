using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace КурсоваяBusinessLogic.Interfaces
{

        public interface IAgentLogic
        {
            List<AgentViewModel> Read(AgentBindingModel model);
            void CreateOrUpdate(AgentBindingModel model);
            void Delete(AgentBindingModel model);
        }
}
