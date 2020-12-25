using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Agent.Models
{
    public class AddReisViewModel
    {
        public int Id { get; set; }
        public int ReisId { get; set; }
        public int DogovorId { get; set; }
        public double NadbavkaCena { get; set; }
        public double NadbavkaTime { get; set; }
        public string Comm { get; set; }
        public double Obem { get; set; }
        public double ves { get; set; }
    }
}
