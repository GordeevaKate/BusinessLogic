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
        [RegularExpression(@"[1-9]{1,}[0-9]{0,}(,[0-9]){0,}", ErrorMessage = "Некорректная цена1")]
        public string Cena1 { get; set; }
        [RegularExpression(@"[1-9]{1,}[0-9]{0,}(,[0-9]){0,}", ErrorMessage = "Некорректная цена2")]
        public string Cena2 { get; set; }
        public SelectList Raion { get; set; }
    }
}
