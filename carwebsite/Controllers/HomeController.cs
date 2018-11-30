using carwebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace carwebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
          
            return View();
        }

        public ActionResult Cars()
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                var types = db.CarTypes.ToList();
                return View(types);
            }          
            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult BookCar(int id)
        {
            CarStoreEntities db = new CarStoreEntities();

            Car Car = db.Cars.Find(id);
          
            Car.Type = "Rented";
            db.Entry(Car).State = EntityState.Modified;
          

            var addedCar = db.Cars
          .Single(car => car.CarId == id);

          
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedCar);
            db.SaveChanges();
            return RedirectToAction("AddressAndPayement", "Home");

        }

        [Authorize]
        public ActionResult AddressAndPayement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayement(FormCollection values)
        {
            CarStoreEntities db = new CarStoreEntities();
            const string PromoCode = "FD1FBN2FMNJT";
            var order = new Booking { };
            var car = new Car { };
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == true)
                {

                    order.Username = User.Identity.Name;
                    order.BookingDate = DateTime.Now;
                    
                    //Save Order
                    db.Bookings.Add(order);
                    db.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order, 0.8m);


                    return RedirectToAction("OrderDetails",
                        new { id = order.BookingId });

                    //return RedirectToAction("OrderDetail", order);

                    //return View(order);
                }
                else
                {
                   
                    order.Username = User.Identity.Name;
                    order.BookingDate = DateTime.Now;
                    order.Status = "Attente";
                  
                    
                    //Save Order
                    db.Bookings.Add(order);
                    db.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order, 1);

                    return RedirectToAction("OrderDetails", new { id = order.BookingId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }


        public ActionResult OrderDetails(int id)
        {
            CarStoreEntities db = new CarStoreEntities();
            ViewBag.total = db.Bookings.Single(o => o.BookingId == id).Total;

            bool isValid = db.Bookings.Any(
                o => o.BookingId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                var oderWithDetails = db.Bookings.Include("BookingDetails").Single(o => o.BookingId == id).BookingDetails.ToList();

                return View(oderWithDetails);
            }
            else
            {
                return View("Error");
            }

        }

        public ActionResult Complete(int id)
        {
            CarStoreEntities db = new CarStoreEntities();
         
            bool isValid = db.Bookings.Any(
                o => o.BookingId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}