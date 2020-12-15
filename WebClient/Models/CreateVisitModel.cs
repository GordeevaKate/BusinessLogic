using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HospitalBusinessLogic.ViewModel;

namespace HospitalWebClient.Models
{
    public class CreateVisitModel
    {
        public Dictionary<int, int> VisitDoctors { get; set; }
    }
}
