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
        [DisplayName("Сумма")]
        public double Summa { get; set; }
        [DataMember]
        [DisplayName("Дата")]
        public DateTime data { get; set; }
        [DataMember]
        [DisplayName("Период")]
        public int Period { get; set; }
    }
}
