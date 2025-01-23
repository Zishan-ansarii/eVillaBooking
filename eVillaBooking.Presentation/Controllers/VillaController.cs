using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository _villaRepository;
        public VillaController(IVillaRepository villaRepository)
        {
            _villaRepository = villaRepository;
        }
        public IActionResult Index()

        {
            return View(_villaRepository.GetAll());
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
                _villaRepository.Add(villa);
                _villaRepository.save();

                TempData["SuccessMessage"] = "Villa has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Edit(int id)
        {
            var villa = _villaRepository.Get(v => v.Id == id);

            if (villa == null)
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
                _villaRepository.Update(villa);
                _villaRepository.save();

                TempData["SuccessMessage"] = "Villa has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Delete(int id)
        {
            var villa = _villaRepository.Get(v => v.Id==id);

            if (villa is null)
            {
                return NotFound();
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var villa = _villaRepository.Get(v => v.Id == id);
            _villaRepository.Remove(villa);
            _villaRepository.save();

            TempData["SuccessMessage"] = "Villa has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

