using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Pasport { get; set; }
        [Required]
        public string ClientFIO { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
