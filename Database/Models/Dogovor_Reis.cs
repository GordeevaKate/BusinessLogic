using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseImplement.Models
{
    public class Dogovor_Reis
    {
        public int Id { get; set; }     
        public int ReisId { get; set; }
        public int DogovorId { get; set; }
        [Required]
        public double Nadbavka { get; set; }
        [Required]
        public string Comm { get; set; }
        [Required]
        public double Obem { get; set; }
        [Required]
        public double ves { get; set; }
        public virtual Reis Reiss { get; set; }
    }
}
