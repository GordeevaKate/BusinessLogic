using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
        [DataContract]
        public class RaionViewModel
    {
            [DataMember]
            public int? Id { get; set; }
            [DataMember]
            [DisplayName("Название")]
            public string Name { get; set; }
    }
    }
