using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareersFair.Models
{

    /// <summary>
    /// The Form model class. holds all the information needed that is an attribute of a form
    /// Created by: Gavan Lamb
    /// Version: 1.0
    /// </summary>
    public class Form
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "A form name is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 30 characters long")]
        public string Name { get; set; }


        [AllowHtml]
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "A form must have elements")]
        [StringLength(999999, MinimumLength = 3, ErrorMessage = "A form must have elements")]
        public string HTML { get; set; }

        public string Storage { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Elements { get; set; }

        public bool Enabled { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Linkedin { get; set; }

        public virtual ICollection<FormResults> FormResults { get; set; }
    }
}