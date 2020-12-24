using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public   class ZarplataBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public double Summa { get; set; }
        [DataMember]
        public DateTime data { get; set; }
        [DataMember]
        public int Period { get; set; }
    }
}
