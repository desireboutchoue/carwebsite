using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace carwebsite.Models
{
    public class CarTypes
    {
        [Key]
        [DisplayName("Type ID")]
        public int CarTypesId { get; set; }



        [DisplayName("Type Name: ")]
        [Required(ErrorMessage = "Cannot be empty")]
        public string Name { get; set; }
        [DisplayName(" description")]
        public string Description { get; set; }
        [DisplayName("Classified goods")]
        public List<Car> Cars { get; set; }
        //public virtual List<Album> Albums { get; set; }

        public string Type { get; set; }


        public IEnumerable<Car>GetTopCar(int count)
        {
            CarStoreEntities db = new CarStoreEntities();
            var Albums = db.Cars.Where(a => a.CarTypesId == CarTypesId).
                OrderByDescending(a => a.BookingDetails.Sum(o => o.Quantity))
                .Take(count)
                .ToList();
            return Albums;
        }
    }

    //should add using WecareMVC.Models in Razor view
    public static class GenreExtension
    {
        public static IEnumerable<Car> GetTopAlbum(this carwebsite.Models.CarTypes cartypes, int count)
        {
            CarStoreEntities db = new CarStoreEntities();
            var Albums = db.Cars.Where(a => a.CarTypesId == cartypes.CarTypesId).
                OrderByDescending(a => a.BookingDetails.Sum(o => o.Quantity))
                .Take(count)
                .ToList();
            return Albums;
        }
    }
}