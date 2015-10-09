using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace careersfair.Models
{
    public class FormResults
    {
        public int ID { get; set; }

        [Column(TypeName = "xml")]
        public string Results { get; set; }

        public virtual Form Form { get; set; }
    }
}