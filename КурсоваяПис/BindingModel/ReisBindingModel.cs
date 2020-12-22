using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{
     [DataContract]
    public class ReisBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int OfId { get; set; }
        [DataMember]
        public int ToId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Cena { get; set; }
        [DataMember]
        public int Time { get; set; }
    }
}
