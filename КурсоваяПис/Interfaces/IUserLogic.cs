using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
     public interface IUserLogic
    {
        List<UserViewModel> Read(UserBindingModel model);
        void CreateOrUpdate(UserBindingModel model);
        void Delete(UserBindingModel model);
    }
}
