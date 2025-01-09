using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var villaNumber = _db.VillaNumbers.ToList();
            var villaNumber = _db.VillaNumbers.Include("Villa").ToList();
            return View(villaNumber);
        }

        public IActionResult Create()
        {
            List<Villa> villas = _db.Villas.ToList();
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
            bool isVillaNumberExist = _db.VillaNumbers.Any(vn => vn.Villa_Number == villaNumber.Villa_Number);
            if (ModelState.IsValid && !isVillaNumberExist)
            {
                _db.VillaNumbers.Add(villaNumber);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Villa Number has been added successfully";
                return RedirectToAction(nameof(Index));
            }

            var selectListItem = _db.Villas.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;
            TempData["ErrorMessage"] = "Villa number already exists";
            return View(villaNumber);
        }

        public IActionResult Edit(int id)
        {
            var villaNumber = _db.VillaNumbers.Find(id);
            if (villaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }
            var selectListItem = _db.Villas.Select(v => new SelectListItem
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
               
                _db.VillaNumbers.Update(villaNumber);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Villa Number has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villaNumber);
        }

        public IActionResult Delete(int id)
        {
            var villaNumber = _db.VillaNumbers.Find(id);
            if (villaNumber is null)
            {
                return NotFound();
            }
            return View(villaNumber);
        }

        public IActionResult DeleteConfirm(int id)
        {
            var villaNumber = _db.VillaNumbers.Find(id);
            _db.VillaNumbers.Remove(villaNumber);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Villa Number has been deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
