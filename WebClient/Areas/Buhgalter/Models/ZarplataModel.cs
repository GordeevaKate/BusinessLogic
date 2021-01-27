using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Buhgalter.Models
{
	public class ZarplataModel
	{
		public int? Id { get; set; }
		public int AgentId { get; set; }
		public double Summ { get; set; }
	}
}
