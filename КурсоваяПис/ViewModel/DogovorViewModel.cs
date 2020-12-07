using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace КурсоваяBusinessLogic.ViewModel
{
     [DataContract]
        public class DogovorViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        [DataMember]
        [DisplayName("Стоимость договора")]
        public double Summa { get; set; }
        [DataMember]
        [DisplayName("Дата")]
        public DateTime data { get; set; }
        public Dictionary<int, (double, string, double, double)> Reiss;
    }
}
