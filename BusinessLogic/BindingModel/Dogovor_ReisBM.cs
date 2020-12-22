using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BindingModel
{
   public class Dogovor_ReisBM
    {
        public int Id { get; set; }
        public int ReisId { get; set; }
        public int DogovorId { get; set; }
        public double Nadbavka { get; set; }
        public string Comm { get; set; }
        public double Obem { get; set; }
        public double ves { get; set; }
    }
}
