using BusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
     [DataContract]
        public class DogovorViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        [DataMember]
        [Column(title: "Сумма договора", gridViewAutoSize: GridViewAutoSize.Fill)]
        public double Summa { get; set; }
        [DataMember]
        [Column(title: "Дата договора", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime data { get; set; }
        public Dictionary<int, (int, int, double,double, string, double, double)> Dogovor_Reiss;

    }
}
