using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace КурсоваяBusinessLogic.BindingModel
{
    [DataContract]
    public class ClientBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string ClientFIO { get; set; }
        [DataMember]
        public string Pasport { get; set; }
        [DataMember]
        public string FIO { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
