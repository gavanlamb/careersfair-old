﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CareersFair.Models
{
    public class FormResults
    {
        public int ID { get; set; }

        [Column(TypeName = "xml")]
        public string Results { get; set; }

        [InverseProperty("ID")]
        [ForeignKey("Form")]
        public int FormId { get; set; }

        public virtual Form Form { get; set; }
    }
}