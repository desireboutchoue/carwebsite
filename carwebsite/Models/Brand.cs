using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace carwebsite.Models
{
    // Brand entity of the car .
    public class Brand
    {
        [DisplayName("Manufacturer ID")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "The Name of the Car cannot be empty")]
        [DisplayName("Manufacturer Name: ")]
        public string Name { get; set; }
    }
}