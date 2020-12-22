using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
        public interface IReisLogic
    {
            List<ReisViewModel> Read(ReisBindingModel model);
            void CreateOrUpdate(ReisBindingModel model);
            void Delete(ReisBindingModel model);
        }
}
