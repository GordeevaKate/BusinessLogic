using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalWebClient.Models
{
    public class PaymentModel
    {
        [Required]
        public int Sum { get; set; }
        public int VisitId { get; set; }
    }
}
