using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace carwebsite.Models
{
    public class Booking
    {
        [ScaffoldColumn(false)]
        public int BookingId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime BookingDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Your Name is required!")]
        [DisplayName("Name :")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your lastname is required!")]
        [DisplayName("Lastname:")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Your address is required!")]
        [StringLength(70)]
        [DisplayName("address：")]
        public string Address { get; set; }

      /*  [Required(ErrorMessage = "Votre ville est requise ")]
        [StringLength(40)]
        [DisplayName("ville：")]
        public string City { get; set; }
        */
        //[Required(ErrorMessage = "*")]
        //[StringLength(40)]
        //[DisplayName("洲")]
        //public string State { get; set; }

      /*  [Required(ErrorMessage = "Votre code postal est réquis")]
        [DisplayName("Code postal：")]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Postal Codeis Required")]
        [StringLength(40)]
        [DisplayName("Pays：")]
        public string Country { get; set; }*/
        [Required(ErrorMessage = " Your Contact is required!")]
        [StringLength(24)]
        [DisplayName("contact：")]
        public string Phone { get; set; }

        public string Status { get; set; }
        [Required(ErrorMessage = "Your email is required!")]
        [DisplayName("Email：")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "incorrect Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<BookingDetail> BookingDetails { get; set; }
    }
}