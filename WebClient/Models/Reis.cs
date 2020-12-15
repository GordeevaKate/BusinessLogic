using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class Reis
    {
        public string OfId { get; set; }
        public string ToId { get; set; }
        public string Name { get; set; }
        public double Cena { get; set; }
        public int Time { get; set; }
    }
}
