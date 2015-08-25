using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace careersfair.Models{

    /// <summary>
    /// The Form model class. holds all the information needed that is an attribute of a form
    /// Created by: Gavan Lamb
    /// Version: 1.0
    /// </summary>
    public class Form{

        public int ID { get; set; }

        [Required(ErrorMessage = "A name must be entered")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "That name is too short")]
        [Remote("IsNameExists", "Form", ErrorMessage = "Form already exists", HttpMethod = "POST")]
        [Editable(true)]
        public string Name { get; set; }

        public string TableName { get; set; }

        public string Structure { get; set; }
    }
}