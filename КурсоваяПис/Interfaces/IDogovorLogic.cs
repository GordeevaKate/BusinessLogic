using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace КурсоваяBusinessLogic.Interfaces
{
        public interface IDogovorLogic
    {
            List<DogovorViewModel> Read(DogovorBindingModel model);
            void CreateOrUpdate(DogovorBindingModel model);
            void Delete(DogovorBindingModel model);
        }
}
