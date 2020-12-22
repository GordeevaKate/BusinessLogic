using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{
        [DataContract]
        public class AgentBindingModel
        {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Oklad { get; set; }
        [DataMember]
        public double Comission { get; set; }
        [DataMember]
        public int UserId { get; set; }
    }
}
