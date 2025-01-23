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
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;
        public VillaNumberController(IVillaNumberRepository villaNumberRepository, IVillaRepository villaRepository)
        {
            _villaNumberRepository = villaNumberRepository;
            _villaRepository = villaRepository;
        }
        public IActionResult Index()
        {
            var villaNumber = _villaNumberRepository.GetAll(includeProperties: "Villa");
            return View(villaNumber);
        }

        public IActionResult Create()
        {
            IEnumerable<Villa> villas = _villaRepository.GetAll();

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
            //bool isVillaNumberExist = _villaNumberRepository.GetAll().Any(vn => vn.Villa_Number == villaNumber.Villa_Number);

            bool isVillaNumberExist = _villaNumberRepository.GetAll(vn => vn.Villa_Number == villaNumber.Villa_Number).Any();

            if (ModelState.IsValid && !isVillaNumberExist)
            {
                _villaNumberRepository.Add(villaNumber);
                _villaNumberRepository.Save();

                TempData["SuccessMessage"] = "Villa Number has been added successfully";
                return RedirectToAction(nameof(Index));
            }

            var selectListItem = _villaRepository.GetAll().Select(v => new SelectListItem
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
            //var villaNumber = _db.VillaNumbers.Find(id);
            var villaNumber = _villaNumberRepository.GetAll(vn => vn.Villa_Number == id).FirstOrDefault();
            if (villaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }
            var selectListItem =_villaRepository.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;
            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult Edit(VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
               
                _villaNumberRepository.Update(villaNumber);
                _villaNumberRepository.Save();

                TempData["SuccessMessage"] = "Villa Number has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villaNumber);
        }

        public IActionResult Delete(int id)
        {
            //var villaNumber = _db.VillaNumbers.FirstOrDefault(vn => vn.Villa_Number==id);
            var villaNumber = _villaNumberRepository.GetAll(vn => vn.Villa_Number == id).FirstOrDefault();
            if (villaNumber is null)
            {
                return NotFound();
            }
            var selectListItem = _villaRepository.GetAll().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;

            return View(villaNumber);
        }

        public IActionResult DeleteConfirm(int id)
        {
            var villaNumber = _villaNumberRepository.Get(vn => vn.Villa_Number == id);
            _villaNumberRepository.Remove(villaNumber);
           _villaNumberRepository.Save();

            TempData["SuccessMessage"] = "Villa Number has been deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
