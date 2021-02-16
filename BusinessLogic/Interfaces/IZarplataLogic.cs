using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IZarplataLogic
    {
        void CreateOrUpdate(ZarplataBindingModel model);
        List<ZarplataViewModel> Read(ZarplataBindingModel model);
        void Delete(ZarplataBindingModel model);
    }
}
