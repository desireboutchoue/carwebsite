using carwebsite.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace carwebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // brand operations  C.R.U.D

        public ActionResult BrandList()
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                return View(db.Brands.ToList());
            }
        }


        public ActionResult AddBrand()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                using (CarStoreEntities db = new CarStoreEntities())
                {
                    db.Brands.Add(brand);
                    db.SaveChanges();
                    return RedirectToAction("BrandList", "Admin");

                }
            }
            return View();
        }

        public ActionResult DeleteBrand(int? id)
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Brand brand = db.Brands.Find(id);
                return View(brand);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrand(int id)
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                Brand brand = db.Brands.Find(id);
                if (ModelState.IsValid)
                {
                    db.Brands.Remove(brand);
                    db.SaveChanges();
                    return RedirectToAction("BrandList", "Admin");
                }

                return View();
            }
        }



        public ActionResult EditBrand(int? id)
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Brand brand = db.Brands.Find(id);
                if (brand == null)
                {
                    return HttpNotFound();
                }
                return View(brand);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBrand([Bind(Include = "Name,BrandId")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                using (CarStoreEntities db = new CarStoreEntities())
                {
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("BrandList", "Admin");
                }
            }
            return View(brand);
        }


        public ActionResult DetailsBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CarStoreEntities db = new CarStoreEntities())
            {
                Brand brand = db.Brands.Find(id);
                if (brand == null)
                {
                    return HttpNotFound();
                }
                return View(brand);
            }
        }




        // CarType operation

        public ActionResult CarTypeList()
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                return View(db.CarTypes.ToList());
            }
        }


        public ActionResult AddCarType()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddCarType(CarTypes cartype)
        {
            if (ModelState.IsValid)
            {
                using (CarStoreEntities db = new CarStoreEntities())
                {
                    db.CarTypes.Add(cartype);
                    db.SaveChanges();
                    return RedirectToAction("CarTypeList", "Admin");

                }
            }
            return View();
        }

        public ActionResult DeleteCarType(int? id)
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CarTypes cartype = db.CarTypes.Find(id);
                return View(cartype);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCarType(int id)
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                CarTypes cartype = db.CarTypes.Find(id);
                if (ModelState.IsValid)
                {
                    db.CarTypes.Remove(cartype);
                    db.SaveChanges();
                    return RedirectToAction("CarTypeList", "Admin");
                }

                return View();
            }
        }



        public ActionResult EditCarType(int? id)
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CarTypes cartype = db.CarTypes.Find(id);
                if (cartype == null)
                {
                    return HttpNotFound();
                }
                return View(cartype);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCarType([Bind(Include = "Name,CarTypesId,Description,Type")] CarTypes cartype)
        {
            if (ModelState.IsValid)
            {
                using (CarStoreEntities db = new CarStoreEntities())
                {
                    db.Entry(cartype).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("CarTypeList", "Admin");
                }
            }
            return View(cartype);
        }


        public ActionResult DetailsCarType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CarStoreEntities db = new CarStoreEntities())
            {
                CarTypes cartype = db.CarTypes.Find(id);
                if (cartype == null)
                {
                    return HttpNotFound();
                }
                return View(cartype);
            }
        }

        public ActionResult AddCar()
        {
            CarStoreEntities db = new CarStoreEntities();

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");
            ViewBag.CarTypesId = new SelectList(db.CarTypes, "CarTypesId", "Name");

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar([Bind(Include = "CarId,CarTypesId,BrandId,Name,Price,CarUrl, CarUrl2 , CarUrl3 ,Class, Type, Description,Fuel,Doors,GearBox ")] Car car, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            CarStoreEntities db = new CarStoreEntities();

            if (ModelState.IsValid && file != null && file2 != null && file3 != null)
            {


                var fileName = Path.GetFileName(file.FileName);
                var fileName2 = Path.GetFileName(file2.FileName);
                var fileName3 = Path.GetFileName(file3.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/Upload"), fileName);
                var path2 = Path.Combine(Server.MapPath("~/Content/images/Upload"), fileName2);
                var path3 = Path.Combine(Server.MapPath("~/Content/images/Upload"), fileName3);

                file.SaveAs(path);
                file2.SaveAs(path2);
                file3.SaveAs(path3);

                car.Type = "Available";
                car.CarUrl = "/Content/images/Upload/" + fileName;
                db.Cars.Add(car);
                car.CarUrl2 = "/Content/images/Upload/" + fileName2;
                db.Cars.Add(car);
                car.CarUrl3 = "/Content/images/Upload/" + fileName3;
                db.Cars.Add(car);
                db.SaveChangesAsync();
                return RedirectToAction("CarList", "Admin");
            }

            ViewBag.ArtistId = new SelectList(db.Brands, "BrandId", "Name", car.CarId);
            ViewBag.GenreId = new SelectList(db.CarTypes, "CarTypesId", "Name", car.CarTypesId);
            ViewBag.uploadInfo = "Please select an Image！";


            return View(car);

        }



        public ActionResult EditCar(int? id)
        {
            CarStoreEntities db = new CarStoreEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", car.CarId);
            ViewBag.CarTypesId = new SelectList(db.CarTypes, "CarTypesId", "Name", car.CarTypesId);
            ViewBag.uploadInfo = "Please select an Image！";
            return View(car);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar([Bind(Include = "CarId,CarTypesId,BrandId,Name,Price,CarUrl, CarUrl2 , CarUrl3 ,Class, Type, Description,Fuel,Doors,GearBox ")] Car car, int id, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            CarStoreEntities db = new CarStoreEntities();
            if (ModelState.IsValid)
            {
                car.CarId = id;


                if (file != null && file2 != null && file3 != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fileName2 = Path.GetFileName(file2.FileName);
                    var fileName3 = Path.GetFileName(file3.FileName);

                    var path = Path.Combine(Server.MapPath("~/Content/images/Upload"), fileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/images/Upload"), fileName2);
                    var path3 = Path.Combine(Server.MapPath("~/Content/images/Upload"), fileName3);

                    file.SaveAs(path);
                    file2.SaveAs(path2);
                    file3.SaveAs(path3);

                    car.CarUrl = "/Content/Images/Upload/" + fileName;
                    car.CarUrl2 = "/Content/images/Upload/" + fileName2;
                    car.CarUrl3 = "/Content/images/Upload/" + fileName3;

                }
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CarList", "Admin");
            }
            ViewBag.ArtistId = new SelectList(db.Brands, "BrandId", "Name", car.CarId);
            ViewBag.GenreId = new SelectList(db.CarTypes, "CarTypesId", "Name", car.CarTypesId);
            ViewBag.uploadInfo = "Please select an Image！";
            return View(car);
        }




        public ActionResult DeleteCar(int? id)
        {
            CarStoreEntities db = new CarStoreEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }


        [HttpPost, ActionName("DeleteCar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarStoreEntities db = new CarStoreEntities();
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("CarList");
        }


        public ActionResult DetailsCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CarStoreEntities db = new CarStoreEntities())
            {
                Car car = db.Cars.Find(id);
                if (car == null)
                {
                    return HttpNotFound();
                }
                return View(car);
            }
        }


        public ActionResult CarList()
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                var cars = db.Cars.Include(a => a.Brand).Include(a => a.CarTypes);
                return View(cars.ToList());
            }
        }


        public ActionResult AvailableCar()
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                var cars = db.Cars.Where(U => U.Type == "Available");
                return View(cars.ToList());
            }
        }


        public ActionResult RentedCar()
        {
            using (CarStoreEntities db = new CarStoreEntities())
            {
                var cars = db.Bookings.Where(U => U.Status == "Attente");
                return View(cars.ToList());
            }
        }


        public ActionResult DetailsAvailableCAr(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CarStoreEntities db = new CarStoreEntities())
            {
                Car car = db.Cars.Find(id);
                if (car == null)
                {
                    return HttpNotFound();
                }
                return View(car);
            }
        }

        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CarStoreEntities db = new CarStoreEntities())
            {
                Booking car = db.Bookings.Find(id);

                db.Bookings.Remove(car);

                Car cars = db.Cars.Find(id);
                cars.Type = "Available";
                db.Entry(cars).State = EntityState.Modified;
                db.SaveChanges();


                if (car == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("AvailableCar", "Admin");
            }
        }


        public ActionResult Users() {
            
            return View(UserManager.Users.ToList());
            }

    }
}