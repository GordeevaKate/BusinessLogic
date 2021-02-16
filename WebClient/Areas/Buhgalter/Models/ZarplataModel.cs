using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Buhgalter.Models
{
	public class ZarplataModel
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public double Summa { get; set; }
		public DateTime Date { get; set; }
	}
}
