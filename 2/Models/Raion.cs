﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseImplement.Models
{
    public class Raion
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
