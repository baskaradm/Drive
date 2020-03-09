using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using Project.MVC.ViewModels;
using Project.Service;

namespace Project.MVC.Controllers
{

        public class VehicleMakeController : Controller
        {
          
            private VehicleContext db = new VehicleContext();
           
            
            // GET: VehicleMake
           public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
           {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.AbbreviationSortParm = sortOrder == "abbreviation" ? "abbreviation_desc" : "Abbreviation";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                var vehicleMakes = from v in db.VehicleMakes
                                   select v;

                if (!String.IsNullOrEmpty(searchString))
                {
                    vehicleMakes = vehicleMakes.Where(v => v.Name.Contains(searchString)
                                                   || v.Abbreviation.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        vehicleMakes = vehicleMakes.OrderByDescending(v => v.Name);
                        break;
                    case "Abbreviation":
                        vehicleMakes = vehicleMakes.OrderBy(v => v.Abbreviation);
                        break;
                    case "abbreviation_desc":
                        vehicleMakes = vehicleMakes.OrderByDescending(v => v.Abbreviation);
                        break;
                    default:
                        vehicleMakes = vehicleMakes.OrderBy(s => s.Name);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);

           /* var vehiclemakes = vehicleMakes.ToPagedList(pageNumber, pageSize);
            var vehiclemakeviewmodels = vehiclemakes.Select(v => new VehicleMakeViewModel
            {
                 ID = v.ID,
                Name = v.Name,
                Abbreviation = v.Abbreviation

            }).ToList();
            */
            IEnumerable<VehicleMakeViewModel> listVehicleMakeViewModels = AutoMapper.Mapper.Map<IEnumerable<VehicleMakeViewModel>>(db.VehicleMakes);
            return View(model: listVehicleMakeViewModels.ToPagedList(pageNumber, pageSize));

            }


            // GET: VehicleMake/Details/1
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleMake vehicleMake = db.VehicleMakes.Find(id);
                if (vehicleMake == null)
                {
                    return HttpNotFound();
                }

                VehicleMakeViewModel vehicleMakeViewModels = AutoMapper.Mapper.Map<VehicleMakeViewModel>(vehicleMake);

                return View(vehicleMakeViewModels);
            }


            // GET: VehicleMake/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: VehicleMake/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Name,Abbreviation")]VehicleMake vehicleMake)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.VehicleMakes.Add(vehicleMake);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (DataException dex)
                {

                    ModelState.AddModelError("Err", "Eror");
                }
            VehicleMakeViewModel vehicleMakeViewModels = AutoMapper.Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeViewModels);

            }


            // GET: VehicleMake/Edit/1
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VehicleMake vehicleMake = db.VehicleMakes.Find(id);
                if (vehicleMake == null)
                {
                    return HttpNotFound();
                }
                VehicleMakeViewModel vehicleMakeViewModels = AutoMapper.Mapper.Map<VehicleMakeViewModel>(vehicleMake);
                return View(vehicleMakeViewModels);


        }

        // POST: VehicleMake/Details/1
        [HttpPost, ActionName("Edit")]
            [ValidateAntiForgeryToken]
            public ActionResult EditPost(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var vehicleMakeToUpdate = db.VehicleMakes.Find(id);
                if (TryUpdateModel(vehicleMakeToUpdate, "",
                    new[] { "Name", "Abbreviation" }))
                {
                    try
                    {
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    catch (DataException dex)
                    {

                        ModelState.AddModelError("Err", "Eror");
                    }
                }
            VehicleMakeViewModel vehicleMakeViewModels = AutoMapper.Mapper.Map<VehicleMakeViewModel>(vehicleMakeToUpdate);
            return View(vehicleMakeViewModels);

        }


        // GET: VehicleMake/Delete/1
        public ActionResult Delete(int? id, bool? saveChangesError = false)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
                }
                VehicleMake vehicleMake = db.VehicleMakes.Find(id);
                if (vehicleMake == null)
                {
                    return HttpNotFound();
                }

            VehicleMakeViewModel vehicleMakeViewModels = AutoMapper.Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeViewModels);

        }

        // POST: VehicleMake/Delete/1
        [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(int id)
            {
                try
                {
                    VehicleMake vehicleMakeToDelete = new VehicleMake { ID = id };
                    db.Entry(vehicleMakeToDelete).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (DataException dex)
                {

                    return RedirectToAction("Delete", new {id, saveChangesError = true });
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

   
