using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IZarplataLogic
    {
        List<ZarplataViewModel> Rascet(int? AgentId, DateTime date);
        void CreateOrUpdate(ZarplataBindingModel model);
    }
}
