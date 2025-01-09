using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()

        {
            return View(_db.Villas.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if(villa.Name == villa.Description)
            {
                ModelState.AddModelError("Name", "Name and Description are same");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(villa);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Villa has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Edit(int id)
        {
            var villa = _db.Villas.Find(id);
            if(villa == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Edit(Villa villa)
        {
            if(ModelState.IsValid)
            {
                _db.Villas.Update(villa);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Villa has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Delete(int id)
        {
            var villa = _db.Villas.Find(id);
            if (villa is null)
            {
                return NotFound();
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var villa = _db.Villas.Find(id);
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Villa has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

