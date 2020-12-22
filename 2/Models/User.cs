using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BusinessLogic.Enums;

namespace DatabaseImplement.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public UserStatus Status { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
