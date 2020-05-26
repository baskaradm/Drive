using AutoMapper;
using PagedList;
using Project.MVC.ViewModels;
using Project.Service.Domain;
using Project.Service.Infrastructure.Helpers;
using Project.Service.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Project.MVC.Controllers
{

    public class VehicleMakeController : Controller
    {

        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

       

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {

            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;

        }


        // GET: VehicleMake
        public async Task<ActionResult> Index(string sortBy, string currentFilter, string searchString, int? page)
        {
            Filtering filters = new Filtering(searchString, currentFilter);
            Sorting sorting = new Sorting(sortBy);
            Paging paging = new Paging(page);

            var vehicleMakes = await  _vehicleMakeService.GetVehicleMakesAsync(filters, sorting, paging);

            List<VehicleMakeViewModel> listVehicleMakeViewModels =
            _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);

            

            IPagedList<VehicleMakeViewModel> paginatedVehicles = new StaticPagedList<VehicleMakeViewModel>(listVehicleMakeViewModels, paging.Page ?? 1, paging.NumberOfObjectsPerPage, paging.TotalCount);
            UpdateView(ViewBag, filters, sorting, paging);

            return View(paginatedVehicles);

        }


        // GET: VehicleMake/Details/1
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMakeByIDAsync(id);

            if (vehicleMake == null)
            {
                return HttpNotFound();
            }

            var vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            
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
        public async Task<ActionResult> Create([Bind(Include = "Name, Abbreviation")]VehicleMake vehicleMakeToInsert)
        {

            if (!await _vehicleMakeService.CreateVehicleMake(vehicleMakeToInsert))
            {

                return View(_mapper.Map<VehicleMakeViewModel>(vehicleMakeToInsert));
                
            }

            return RedirectToAction("Index");

        }


        // GET: VehicleMake/Edit/1
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMakeByIDAsync(id);

            if (vehicleMake == null)
            {
                return HttpNotFound();
            }

            VehicleMakeViewModel vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
           

            return View(vehicleMakeViewModels);

        }

        // POST: VehicleMake/Edit/1
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVehicleMake(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicleMakeToUpdate = await _vehicleMakeService.GetVehicleMakeByIDAsync(id);

            if (TryUpdateModel(vehicleMakeToUpdate, "", new[] { "Name", "Abbreviation" }))
            {
                try
                {
                    await _vehicleMakeService.EditVehicle(vehicleMakeToUpdate);
                    

                    return RedirectToAction("Index");
                }
                catch (DataException dex)
                {

                    ModelState.AddModelError("Err", dex);
                }
            }

            VehicleMakeViewModel vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel>(vehicleMakeToUpdate);
            
            return View(vehicleMakeViewModels);
       
        }


        // GET: VehicleMake/Delete/1
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

            VehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMakeByIDAsync(id);

            if (vehicleMake == null)
            {
                return HttpNotFound();
            }

            VehicleMakeViewModel vehicleMakeViewModels = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(vehicleMakeViewModels);

        }

        // POST: VehicleMake/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {

                VehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMakeByIDAsync(id);
                await _vehicleMakeService.DeleteVehicleMake(vehicleMake);

            }

            catch (DataException)
            {

                return RedirectToAction("Delete", new { id, saveChangesError = true });
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



