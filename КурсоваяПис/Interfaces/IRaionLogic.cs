using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace КурсоваяBusinessLogic.Interfaces
{
        public interface IRaionLogic
    {
            List<RaionViewModel> Read(RaionBindingModel model);
        //    void CreateOrUpdate(RaionBindingModel model);
       //     void Delete(RaionBindingModel model);
        }
}
