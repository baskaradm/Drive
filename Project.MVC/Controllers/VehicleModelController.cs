using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Project.MVC.ViewModels;
using Project.Service;
using PagedList;


namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {

        private readonly VehicleContext db = new VehicleContext();


        // GET: VehicleModel
        public ActionResult Index(string sortBy, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            ViewBag.SortByAbbreviation = sortBy == "Abbreviation" ? "abbreviation_desc" : "Abbreviation";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var vehicleModels = from v in db.VehicleModels
                select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = vehicleModels.Where(v => v.Name.Contains(searchString)
                                                         || v.Abbreviation.Contains(searchString));
            }

            switch (sortBy)
            {
                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Name);
                    break;
                case "Abbreviation":
                    vehicleModels = vehicleModels.OrderBy(v => v.Abbreviation);
                    break;
                case "abbreviation_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Abbreviation);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            IEnumerable<VehicleModelViewModel> listVehicleModelViewModels =
                AutoMapper.Mapper.Map<IEnumerable<VehicleModelViewModel>>(db.VehicleModels);
            return View(model: listVehicleModelViewModels.ToPagedList(pageNumber, pageSize));


        }



        // GET: VehicleModel/Details/1
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }

            VehicleModelViewModel vehicleModelViewModel = AutoMapper.Mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelViewModel);
        }



        // GET: VehicleModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Abbreviation")] VehicleModel vehicleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.VehicleModels.Add(vehicleModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {

                ModelState.AddModelError("Err", "Eror.");
            }

            VehicleModelViewModel vehicleModelViewModel = AutoMapper.Mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelViewModel);
        }


        // GET: VehicleModel/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }

            VehicleModelViewModel vehicleModelViewModel = AutoMapper.Mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelViewModel);
        }


        // POST: VehicleModel/Edit/1
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicleModelToUpdate = db.VehicleModels.Find(id);
            if (TryUpdateModel(vehicleModelToUpdate, "",
                new string[] {"Name", "Abbreviation"}))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException dex)
                {

                    ModelState.AddModelError("Err", "Eror.");
                }
            }

            VehicleModelViewModel vehicleModelViewModel =
                AutoMapper.Mapper.Map<VehicleModelViewModel>(vehicleModelToUpdate);
            return View(vehicleModelViewModel);
        }


        // GET: VehicleModel/Delete/1
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage =
                    "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }

            VehicleModelViewModel vehicleModelViewModel = AutoMapper.Mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelViewModel);
        }

        // POST: VehicleModel/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                VehicleMake vehicleModelToDelete = new VehicleMake() {ID = id};
                db.Entry(vehicleModelToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (DataException dex)
            {

                return RedirectToAction("Delete", new {id = id, saveChangesError = true});
            }

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}





