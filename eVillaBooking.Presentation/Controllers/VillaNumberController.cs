using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villaNumber = _unitOfWork.VillaNumberRepositoryUOW.GetAll(includeProperties: "Villa");
            return View(villaNumber);
        }

        public IActionResult Create()
        {
            IEnumerable<Villa> villas = _unitOfWork.VillaRepositoryUOW.GetAll();

            IEnumerable<SelectListItem> selectListItems = villas.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
            ViewData["SelectListItem"] = selectListItems;
            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaNumber villaNumber)
        {

            bool isVillaNumberExist = _unitOfWork.VillaNumberRepositoryUOW.GetAll(vn => vn.Villa_Number == villaNumber.Villa_Number).Any();

            if (ModelState.IsValid && !isVillaNumberExist)
            {
                _unitOfWork.VillaNumberRepositoryUOW.Add(villaNumber);
                _unitOfWork.Save();

                TempData["SuccessMessage"] = "Villa Number has been added successfully";
                return RedirectToAction(nameof(Index));
            }

            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewBag.SelectListItem = selectListItem;
            TempData["ErrorMessage"] = "Villa number already exists";
            return View(villaNumber);
        }

        public IActionResult Edit(int id)
        {
            var villaNumber = _unitOfWork.VillaNumberRepositoryUOW.GetAll(vn => vn.Villa_Number == id).FirstOrDefault();

            if (villaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }

            var selectListItem =_unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;
            return View(villaNumber);
        }

        //[HttpPost]
        //public IActionResult Edit(VillaNumber villaNumber)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.VillaNumberRepositoryUOW.Update(villaNumber);
        //        _unitOfWork.Save();

        //        TempData["SuccessMessage"] = "Villa Number has been updated successfully";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(villaNumber);
        //}

        [HttpPost]
        public IActionResult Edit(VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the original entity from the database to ensure EF is tracking it
                var existingVillaNumber = _unitOfWork.VillaNumberRepositoryUOW.Get(v => v.Villa_Number == villaNumber.Villa_Number);

                if (existingVillaNumber != null)
                {
                    // Update only the fields that are allowed to change
                    existingVillaNumber.SpecialDetails = villaNumber.SpecialDetails;
                    existingVillaNumber.Villa_Id = villaNumber.Villa_Id;

                    // Save changes to the database
                    _unitOfWork.Save();

                    TempData["SuccessMessage"] = "Villa Number has been updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "The specified Villa Number does not exist.");
                }
            }
            return View(villaNumber);
        }


        public IActionResult Delete(int id)
        {
            var villaNumber = _unitOfWork.VillaNumberRepositoryUOW.Get(vn => vn.Villa_Number == id);

            if (villaNumber is null)
            {
                return NotFound();
            }

            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;

            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var villaNumber = _unitOfWork.VillaNumberRepositoryUOW.Get(vn => vn.Villa_Number == id);

            _unitOfWork.VillaNumberRepositoryUOW.Remove(villaNumber);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = "Villa Number has been deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
