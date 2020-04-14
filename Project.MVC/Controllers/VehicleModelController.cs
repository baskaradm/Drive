using AutoMapper;
using PagedList;
using Project.MVC.Infrastructure;
using Project.MVC.ViewModels;
using Project.Service;
using Project.Service.Domain;
using Project.Service.Implementations;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Project.MVC.Controllers
{

    public class VehicleModelController : Controller
    {
        private readonly IMapper _mapper;
        private IVehicleModelService _vehicleModelService;

        public VehicleModelController()
        {
            _vehicleModelService = new VehicleModelService(new ModelStateWrapper(this.ModelState),
            new VehicleModelRepository());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = new Mapper(config);
        }

        public VehicleModelController(IVehicleModelService vehicleModelService, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleModelService = vehicleModelService;
        }


        // GET: VehicleModel
        public async Task<ActionResult> Index(string sortBy, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortBy;
            ViewBag.SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            ViewBag.SortByAbbreviation = sortBy == "Abbreviation" ? "abbreviation_desc" : "Abbreviation";
            ViewBag.SortById = sortBy == "VehicleMakeId" ? "vehiclemakeid_desc" : "VehicleMakeId";

            if (searchString != null)
            {

                page = 1;
            }

            else
            {

                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            

            


            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var vehicleModels = await _vehicleModelService.GetVehicleModelsAsync(sortBy, currentFilter, searchString);

            var vehicles = vehicleModels.ToList();

            List<VehicleModelViewModel> listVehicleModelViewModels =
              _mapper.Map<List<VehicleModelViewModel>>(vehicles);

            return View(model: listVehicleModelViewModels.ToPagedList(pageNumber, pageSize));
        } 


        // GET: VehicleModel/Details/1
        public async Task <ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModel vehicleModel =  await _vehicleModelService.GetVehicleModelByIDAsync(id);

            if (vehicleModel == null)
            {

                return HttpNotFound();
            }

            VehicleModelViewModel vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

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
        public async Task <ActionResult> Create([Bind(Include = "Id, Name,Abbreviation,VehicleMakeId")] VehicleModel vehicleModelToInsert)
        {
            if (!await _vehicleModelService.CreateVehicleModel(vehicleModelToInsert))
            
              
                return View ( _mapper.Map<VehicleModelViewModel>(vehicleModelToInsert));
               
            
           
            return RedirectToAction("Index");
     
        }


        // GET: VehicleModel/Edit/1
        public async Task <ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModel vehicleModel = await _vehicleModelService.GetVehicleModelByIDAsync(id);

            if (vehicleModel == null)
            {

                return HttpNotFound();
            }

            VehicleModelViewModel vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return View(vehicleModelViewModel);
        }


        // POST: VehicleModel/Edit/1
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> EditVehicleModel(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicleModelToUpdate = await _vehicleModelService.GetVehicleModelByIDAsync(id);

            if (TryUpdateModel(vehicleModelToUpdate, "", new string[] {"VehicleMakeId", "Name", "Abbreviation"}))
            {
                try
                {
                    await _vehicleModelService.EditVehicleModel(vehicleModelToUpdate);

                    return RedirectToAction("Index");
                }

                catch (DataException dex)
                {

                    ModelState.AddModelError("Err", dex);
                }
            }

            VehicleModelViewModel vehicleModelViewModel =
            _mapper.Map<VehicleModelViewModel>(vehicleModelToUpdate);

            return View(vehicleModelViewModel);

        }


        // GET: VehicleModel/Delete/1
        public async Task <ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {

                ViewBag.ErrorMessage ="Failed to delete the item. Please try again.";
            }

            VehicleModel vehicleModel = await _vehicleModelService.GetVehicleModelByIDAsync(id);

            if (vehicleModel == null)
            {

                return HttpNotFound();
            }

            VehicleModelViewModel vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return View(vehicleModelViewModel);

        }


        // POST: VehicleModel/Delete/1
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                VehicleModel vehicleModel = await _vehicleModelService.GetVehicleModelByIDAsync(id);
                await _vehicleModelService.DeleteVehicleModel(vehicleModel);
                
            }

            catch (DataException)
            {

                return RedirectToAction("Delete", new {id = id, saveChangesError = true});

            }

            return RedirectToAction("Index");
        }

    }

}





