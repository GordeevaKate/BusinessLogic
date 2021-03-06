﻿using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;

namespace BusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
