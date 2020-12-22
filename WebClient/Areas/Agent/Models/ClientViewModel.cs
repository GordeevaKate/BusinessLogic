using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Agent.Models
{
    public class ClientViewModel 
    {
        public string Passport { get; set; }
        public int? DogovorId { get; set; }
    }
}
