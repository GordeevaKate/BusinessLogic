using BusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class Dogovor_ReisVM
    {
        [DataMember]
        public int? Id { get; set; }
        public int DogovorId { get; set; }
        public int ReisId { get; set; }
        [DataMember]
        [Column(title: "Объем договора", gridViewAutoSize: GridViewAutoSize.Fill)]
        public double Obem { get; set; }
        [DataMember]
        [Column(title: "Вес договора", gridViewAutoSize: GridViewAutoSize.Fill)]
        public double ves { get; set; }
        [DataMember]
        [Column(title: "Comm", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Comm { get; set; }
        [DataMember]
        [Column(title: "Надбавка цены", gridViewAutoSize: GridViewAutoSize.Fill)]
        public double NadbavkaCena { get; set; }
        [DataMember]
        [Column(title: "Надбавка времени", gridViewAutoSize: GridViewAutoSize.Fill)]
        public double NadbavkaTime { get; set; }
    }
}
