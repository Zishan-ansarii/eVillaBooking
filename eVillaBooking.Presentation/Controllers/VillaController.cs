using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()

        {
            return View(_unitOfWork.VillaRepositoryUOW.GetAll());
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
                if(villa.Image != null)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string imagePath = Path.Combine(webRootPath, @"Images\VillaImages");
                    string myFileName = villa.Image.FileName.Split('.')[0] +"_"+ Guid.NewGuid().ToString().Substring(0,5) + Path.GetExtension(villa.Image.FileName);

                    string finalPath = Path.Combine(imagePath, myFileName);
                    using (FileStream filestream = new FileStream(finalPath,FileMode.Create))
                    {
                        villa.Image.CopyTo(filestream);
                        villa.ImageUrl = @"\Images\VillaImages\" + myFileName;
                    }
                }
                else
                {
                    villa.ImageUrl = "www.dummy.com";
                }


                _unitOfWork.VillaRepositoryUOW.Add(villa);
                _unitOfWork.Save();

                TempData["SuccessMessage"] = "Villa has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Edit(int id)
        {
            var villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == id);

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
                _unitOfWork.VillaRepositoryUOW.Update(villa);
                _unitOfWork.Save();

                TempData["SuccessMessage"] = "Villa has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Delete(int id)
        {
            var villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id==id);

            if (villa is null)
            {
                return NotFound();
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == id);
            _unitOfWork.VillaRepositoryUOW.Remove(villa);
            _unitOfWork.Save();

            TempData["SuccessMessage"] = "Villa has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

