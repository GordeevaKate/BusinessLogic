using System;

namespace WebClient.Areas.Agent.Models
{
    public class ReportModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool SendMail { get; set; }
    }
}
