using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace carwebsite.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int CarId { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = " ")]  //空白可以不顯示錯誤訊息 又可以不讓使用者輸空白
        [Range(0, 100, ErrorMessage = "The quantity should be between doit 0 ~ 100")]
        [DisplayName("Quantity")]
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Car Car { get; set; }
    }

    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int BookingId { get; set; }
        public int CarId { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Unit price")]
        public decimal UnitPrice { get; set; }
        public virtual Car Car { get; set; }
        public virtual Booking Booking { get; set; }
    }
}