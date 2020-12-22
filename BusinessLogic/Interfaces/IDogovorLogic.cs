using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
        public interface IDogovorLogic
    {
        List<DogovorViewModel> Read(DogovorBindingModel model);
        void CreateOrUpdate(DogovorBindingModel model);
        void Delete(DogovorBindingModel model);
        List<DogovorViewModel> Rascet(int? AgentId, DateTime date);
        void AddReis(Dogovor_ReisBM model);
        void DeleteReisDogovor(Dogovor_ReisBM model);
        List<DogovorViewModel> ReadReis(Dogovor_ReisBM model);
    }
}
