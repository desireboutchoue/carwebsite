using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace carwebsite.Models
{
    public class CarStoreEntities : DbContext
    {
        public CarStoreEntities()
           : base("name=CarStoreEntities1")
        {
        }



        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> CartsCar { get; set; }
        public DbSet<CarTypes> CarTypes { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        /*   public DbSet<Cart> Carts { get; set; }
           public DbSet<Order> Orders { get; set; }
           public DbSet<OrderDetail> OrderDetails { get; set; }


           public DbSet<MatiereList> matiereLists { get; set; }
           public DbSet<CouleurList> couleurLists { get; set; }
           public DbSet<PointurList> pointurLists { get; set; }
           public DbSet<TaillList> taillLists { get; set; }*/




    }
}