using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace КурсоваяBusinessLogic.Interfaces
{
        public interface IReisLogic
    {
            List<ReisViewModel> Read(ReisBindingModel model);
            void CreateOrUpdate(ReisBindingModel model);
            void Delete(ReisBindingModel model);
        }
}
