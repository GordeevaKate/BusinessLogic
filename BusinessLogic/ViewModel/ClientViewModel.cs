using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]

        public int UserId { get; set; }
        [DataMember]
        [DisplayName("Паспортные данные")]
        public string Pasport { get; set; }
        [DataMember]
        [DisplayName("ФИО")]
        public string ClientFIO { get; set; }
        [DataMember]
        [DisplayName("Почта")]
        public string Email { get; set; }
        [DataMember]
        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }

    }
}
