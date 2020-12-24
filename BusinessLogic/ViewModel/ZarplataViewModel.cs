using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
   public class ZarplataViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public double Summa { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public DateTime data { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public int Period { get; set; }
    }
}
