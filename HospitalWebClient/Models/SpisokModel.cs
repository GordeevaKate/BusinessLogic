﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class SpisokModel
    {
        public double Cena1 { get; set; }
        public string Raion { get; set; }
        public double Cena2 { get; set; }
        public IEnumerable<Reis> Reis { get; set; }
   //     public SelectList Teams { get; set; }
   //     public SelectList Positions { get; set; }
    }
}