using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalBusinessLogic.Enums;

namespace HospitalWebClient.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int Duration { get; set; }
        public DateTime DateOfBuying { get; set; }
        public int FinalCost { get; set; }
        public int LeftSum { get; set; }
        public VisitStatus Status { get; set; }
        public List<VisitDoctorModel> VisitDoctors { get; set; }
    }
}
