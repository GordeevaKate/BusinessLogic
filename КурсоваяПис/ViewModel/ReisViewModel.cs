using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public  class ReisViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int OfId { get; set; }
        [DataMember]
        public int ToId { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public double Cena { get; set; }
        [DataMember]
        [DisplayName("Продолжительность")]
        public int Time { get; set; }
    }
}
