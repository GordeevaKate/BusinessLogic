using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
        public interface IRaionLogic
    {
            List<RaionViewModel> Read(RaionBindingModel model);
        //    void CreateOrUpdate(RaionBindingModel model);
       //     void Delete(RaionBindingModel model);
        }
}
