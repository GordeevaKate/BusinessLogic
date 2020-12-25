using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Areas.Agent.Models
{
    public class AddClientModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите логин")]
        [StringLength(50, ErrorMessage = "Логин должен содержать от 1 до 50 символов", MinimumLength = 1)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        public string Pasport { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите E-Mail")]
        [EmailAddress(ErrorMessage = "Вы ввели некорректный E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите телефон")]
        [RegularExpression(@"^([\+]?(?:00)?[0-9]{1,3}[\s.-]?[0-9]{1,12})([\s.-]?[0-9]{1,4}?)$", ErrorMessage = "Вы ввели некорректный номер телефона")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите ФИО")]
        public string ClientFIO { get; set; }
    }
}
