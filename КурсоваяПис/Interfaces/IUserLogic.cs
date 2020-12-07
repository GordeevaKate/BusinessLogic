using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace КурсоваяBusinessLogic.Interfaces
{
     public interface IUserLogic
    {
        List<UserViewModel> Read(UserBindingModel model);
        void CreateOrUpdate(UserBindingModel model);
        void Delete(UserBindingModel model);
    }
}
