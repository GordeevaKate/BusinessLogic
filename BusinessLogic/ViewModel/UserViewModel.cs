using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
        [DataContract]
        public class UserViewModel
        {
            [DataMember]
            public int Id { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public UserStatus Status { get; set; }
        [DataMember]
            [DisplayName("Логин")]
            public string Login { get; set; }
            [DisplayName("Пароль")]
            public string Password { get; set; }
        }
     }
