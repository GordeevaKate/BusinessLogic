using КурсоваяBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace КурсоваяBusinessLogic.BindingModel
{
 
        [DataContract]
        public class UserBindingModel
        {
            [DataMember]
            public int? Id { get; set; }
            [DataMember]
            public string Login { get; set; }
            [DataMember]
            public UserStatus Status { get; set; }
            [DataMember]
            public string Password { get; set; }

        }
}
