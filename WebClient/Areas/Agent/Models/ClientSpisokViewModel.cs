using System.ComponentModel.DataAnnotations;
namespace WebClient.Areas.Agent.Models
{
    public class ClientSpisokViewModel 
    {
        public string Passport { get; set; }
        public int DogovorId { get; set; }
        public string Obem { get; set; }
    }
}
