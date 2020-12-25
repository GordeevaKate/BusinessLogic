using System;
using System.Collections.Generic;
using System.Text;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using BusinessLogic.ViewModel;

namespace BusinessLogic.HelperModels
{
    public class Info
    {
        public string FileName { get; set; }
        public string Client { get; set; }
        public string Agent { get; set; }
        public DogovorViewModel dogovor { get; set; }
        public List< DogovorViewModel> dogovors{ get; set; }
        public List<Dogovor_ReisVM> dogovor_Reis { get; set; }
        public List<ReisViewModel> reiss { get; set; }
        public List<RaionViewModel> raion { get; set; }
        public string Title { get; set; }
        public List<string> Colon { get; set; }
        public List<ClientViewModel> Clients { get; set; }
    }
}
