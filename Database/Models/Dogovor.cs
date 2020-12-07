using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class Dogovor
    {
        public int? Id { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        [Required]
        public double Summa { get; set; }
        [Required]
        public DateTime data { get; set; }
        public virtual List<Dogovor_Reis> Dogovor_Reiss{ get; set; }

    }
}
