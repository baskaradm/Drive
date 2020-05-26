using AutoMapper;
using PagedList;
using Project.MVC.ViewModels;
using Project.Service.Domain;
using Project.Service.Infrastructure.Helpers;
using Project.Service.Interfaces;
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

        public VehicleModelController(IVehicleModelService vehicleModelService, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleModelService = vehicleModelService;
        }


        // GET: VehicleModel
        public async Task<ActionResult> Index(string sortBy, string currentFilter, string searchString, int? page)
        {
            Filtering filters = new Filtering(searchString, currentFilter);
            Sorting sorting = new Sorting(sortBy);
            Paging paging = new Paging(page);

            IEnumerable<VehicleModel> vehicleModels = await _vehicleModelService.GetVehicleModelsAsync(filters, sorting, paging);
            
            IEnumerable<VehicleModelViewModel> listVehicleModelViewModels =
            _mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicleModels);

            IPagedList<VehicleModelViewModel> paginatedVehicles = new StaticPagedList<VehicleModelViewModel>(listVehicleModelViewModels, paging.Page ?? 1, paging.NumberOfObjectsPerPage, paging.TotalCount);

            UpdateView(ViewBag, filters, sorting, paging);

            return View(paginatedVehicles);
        }


        // GET: VehicleModel/Details/1
        public async Task<ActionResult> Details(int? id)
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


        // GET: VehicleModel/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: VehicleModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id, Name,Abbreviation,VehicleMakeId")] VehicleModel vehicleModelToInsert)
        {
            if (!await _vehicleModelService.CreateVehicleModel(vehicleModelToInsert))


                return View(_mapper.Map<VehicleModelViewModel>(vehicleModelToInsert));



            return RedirectToAction("Index");

        }


        // GET: VehicleModel/Edit/1
        public async Task<ActionResult> Edit(int? id)
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
        public async Task<ActionResult> EditVehicleModel(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicleModelToUpdate = await _vehicleModelService.GetVehicleModelByIDAsync(id);

            if (TryUpdateModel(vehicleModelToUpdate, "", new string[] { "VehicleMakeId", "Name", "Abbreviation" }))
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
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {

                ViewBag.ErrorMessage = "Failed to delete the item. Please try again.";
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                VehicleModel vehicleModel = await _vehicleModelService.GetVehicleModelByIDAsync(id);
                await _vehicleModelService.DeleteVehicleModel(vehicleModel);

            }

            catch (DataException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });

            }

            return RedirectToAction("Index");
        }

        [NonAction]
        private void UpdateView(dynamic ViewBag, Filtering filters, Sorting sorting, Paging paging)
        {
            ViewBag.CurrentSort = sorting.SortBy;
            ViewBag.SortByName = sorting.SortByName;
            ViewBag.SortByAbbreviation = sorting.SortByAbbreviation;
            if (filters.SearchString != null)
            {

                paging.Page = 1;
            }

            else
            {

                filters.SearchString = filters.CurrentFilter;
            }

            ViewBag.CurrentFilter = filters.SearchString;
        }

    }

}
