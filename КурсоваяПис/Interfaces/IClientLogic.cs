using System;
using System.Collections.Generic;
using System.Text;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.ViewModel;

namespace КурсоваяBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
