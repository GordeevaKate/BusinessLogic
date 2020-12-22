using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{
     [DataContract]
    public class DogovorBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int AgentId { get; set; }
        [DataMember]
        public double Summa { get; set; }
        [DataMember]
        public DateTime data { get; set; }
        public Dictionary<int, (int,int,double,double,string,double, double)> Dogovor_Reiss;
     }
}
