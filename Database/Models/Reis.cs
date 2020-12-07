using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class Reis
    {
        public int? Id { get; set; }
        public int OfId { get; set; }
        public int ToId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Cena { get; set; }
        [Required]
        public int Time { get; set; }
        public virtual List<Dogovor_Reis> Dogovor_Reiss { get; set; }
    }
}
