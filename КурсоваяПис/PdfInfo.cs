using System;
using System.Collections.Generic;
using System.Text;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using BusinessLogic.ViewModel;

namespace BusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<string> Colon { get; set; }
        public List<ClientViewModel> Components { get; set; }
    }
}
