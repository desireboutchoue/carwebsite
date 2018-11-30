using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace carwebsite.Models
{
    [Bind(Exclude = "CarId")]
    public class Car
    {
        [ScaffoldColumn(false)]
        public int CarId { get; set; }
        [DisplayName("Manufacturer ID")]
        public int BrandId { get; set; }

        [DisplayName("Product brand ID")]
        public int CarTypesId { get; set; }
        [Required(ErrorMessage = "Name cannot be empty!")]
        [StringLength(160)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price cannot be empty!")]
        [Range(0.01, 10000000.00,
        ErrorMessage = "Price should be between 1 and 1000 !")]
        [DisplayName("Unit price")]
        public decimal Price { get; set; }
        [DisplayName("image")]
        [StringLength(1024)]
        public string CarUrl { get; set; }
        [DisplayName("second image")]
        [StringLength(1024)]
        public string CarUrl2 { get; set; }

        [DisplayName("Third image")]
        [StringLength(1024)]
        public string CarUrl3 { get; set; }

        [Required(ErrorMessage = "Class cannot be empty")]
        public string Class { get; set; }
        [Required(ErrorMessage = "Fuel cannot be empty")]
        public string Fuel { get; set; }
        [Required(ErrorMessage = "Doors cannot be empty")]
        public int Doors { get; set; }
        [Required(ErrorMessage = "Gearbox cannot be empty")]
        public string GearBox { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }



        public int PointurListId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual CarTypes CarTypes { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }

    }


}
