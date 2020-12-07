using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class Agent
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Oklad { get; set; }
        [Required]
        public double Comission { get; set; }
        public int UserId { get; set; }
    }
}
