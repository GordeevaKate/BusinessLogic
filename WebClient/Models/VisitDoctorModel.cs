using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalWebClient.Models
{
    public class VisitDoctorModel
    {
        public string DoctorName { get; set; }
        public int Cost { get; set; }
        public int Duration { get; set; }
        public string Specialication { get; set; }
        public int Count { get; set; }
    }
}
