﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{ 
        [DataContract]
        public class RaionBindingModel
        {
            [DataMember]
            public int? Id { get; set; }
            [DataMember]
            public string Name { get; set; }
        }
}
