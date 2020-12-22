using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
  [DataContract]
    public class AgentViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Оклад")]
        public double Oklad { get; set; }
        [DataMember]
        [DisplayName("Комиссия")]
        public double Comission { get; set; }
        [DataMember]
        public int UserId { get; set; }
    }
}
