using Database.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebClient.Models
{
    public class SpisokModel
    {

        public string Cena1 { get; set; }

        public string Cena2 { get; set; }
        public SelectList Raion { get; set; }
    }
}
