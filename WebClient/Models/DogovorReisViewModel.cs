using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class DogovorReisViewModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите надбавку")]
        [StringLength(3, ErrorMessage = "Надбавка не должна быть большее 999", MinimumLength = 1)]
        [RegularExpression(@"^?[1-9]?[0-9]{1,3}(.[0-9]{1,3})?)$", ErrorMessage = "Вы ввели некорректный надбавку")]
        public string Nadbavka { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите вес")]
        [RegularExpression(@"^?[1-9]?[0-9]{1,3}(.[0-9]{1,3})?)$", ErrorMessage = "Вы ввели некорректный вес")]
        public string Ves { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите объем")]
        [RegularExpression(@"^?[1-9]?[0-9]{1,3}(.[0-9]{1,3})?)$", ErrorMessage = "Вы ввели некорректный объем")]
        public string Obem { get; set; }

        [StringLength(50, ErrorMessage = "Комментарий не должна быть превышать 50 символов")]
        public string Comm { get; set; }
    }
}
