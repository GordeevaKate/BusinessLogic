using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseImplement.Models
{
  public  class Zarplata
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Summa { get; set; }
        [Required]
        public DateTime data { get; set; }
        [Required]
        public int Period { get; set; }

    }
}
