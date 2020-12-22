using System.ComponentModel.DataAnnotations;
namespace WebClient.Areas.Agent.Models
{
    public class SpisokClientViewModel 
    {
        public string Passport { get; set; }
        public int? DogovorId { get; set; }
        public string Obem { get; set; }
    }
}
