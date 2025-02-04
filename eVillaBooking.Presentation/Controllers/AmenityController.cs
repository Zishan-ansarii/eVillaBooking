using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVillaBooking.Presentation.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.AmenityRepositoryUOW.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            IEnumerable<Villa> villas = _unitOfWork.VillaRepositoryUOW.GetAll();

            IEnumerable<SelectListItem> selectListItems = villas.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            ViewBag.SelectListItem = selectListItems;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Amenity amenity)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepositoryUOW.Add(amenity);
                _unitOfWork.Save();

                TempData["SuccessMessage"] = "Amenities has been added successfully";
                return RedirectToAction(nameof(Index));
            }

            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewBag.SelectListItem = selectListItem;
            TempData["ErrorMessage"] = "Villa number could not be created";
            return View(amenity);
        }

        public IActionResult Edit(int id)
        {
            var amenity = _unitOfWork.AmenityRepositoryUOW.Get(am => am.Id == id);

            if (amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }

            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewBag.SelectListItem = selectListItem;
            return View(amenity);
        }

        [HttpPost]
        public IActionResult Edit(Amenity amenity)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepositoryUOW.Update(amenity);
                _unitOfWork.Save();

                TempData["SuccessMessage"] = "Amenity has been updated successfully";
                return RedirectToAction(nameof(Index));
            }

            var selectListItem = _unitOfWork.AmenityRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewBag.SelectListItem = selectListItem;
            return View(amenity);

        }


        public IActionResult Delete(int id)
        {
            var amenity = _unitOfWork.AmenityRepositoryUOW.Get(am => am.Id == id);

            if (amenity is null)
            {
                return NotFound();
            }

            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;
            return View(amenity);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var amenity = _unitOfWork.AmenityRepositoryUOW.Get(am => am.Id == id);

            _unitOfWork.AmenityRepositoryUOW.Remove(amenity);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = "Amenity has been deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
