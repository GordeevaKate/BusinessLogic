using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Buhgalter.Models
{
	public class AgentListModel
	{
		public int? Id { get; set; }
		public string FIO { get; set; }
		public double Oklad { get; set; }
		public double Comission { get; set; }
	}
}
