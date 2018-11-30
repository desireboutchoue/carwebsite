using carwebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace carwebsite.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult CarTypeMenu()
        {
            //var genres = from p in storeDB.Genres
            //             orderby p.Name ascending
            //             select new GenreViewModel
            //             {  AlbumsCount = p.Albums.Count, GenreName =p.Name };

            using (CarStoreEntities db = new CarStoreEntities())
            {
                var cartype = db.CarTypes.Include("Cars").OrderBy(g => g.Name);
                var model = cartype.ToList();
                return PartialView("_CarTypeMenu", model);

            }

        }

        public ActionResult Browse(int? page, string genre = "Hot")
        {
            CarStoreEntities db = new CarStoreEntities();

            // Retrieve Types and its Associated car from database
            var genreModel = db.CarTypes.Include("Cars")
            .Single(g => g.Name == genre).Cars;

            return View(genreModel.ToList().ToPagedList(page ?? 1, 4));
        }

        public ActionResult DetailsCar(int id)
        {
            CarStoreEntities db = new CarStoreEntities();
            var car = db.Cars.Find(id);
            return View(car);
        }

      

    }
}