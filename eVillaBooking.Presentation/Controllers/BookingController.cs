using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Application.Utility;
using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eVillaBooking.Presentation.Controllers
{
    public class BookingController : Controller

    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IActionResult FinalizeBooking(DateOnly checkInDate, int nights, int villaId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            ApplicationUser applicationUser = _unitOfWork.ApplicationUserRepositoryUOW.Get(au => au.Id == userId);
            Booking booking = new()
            {
                CheckInDate = checkInDate,
                Nights = nights,
                VillaId = villaId,
                CheckOutDate = checkInDate.AddDays(nights),
                Villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == villaId, includeProperties: "AmenityList"),
                Email = applicationUser.Email!,
                Name = applicationUser.Name,
                Phone = applicationUser.PhoneNumber,
                UserId = userId
            };


            booking.TotalCost = booking.Villa.Price * nights;
            return View(booking);
        }

        [Authorize,HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {
            Villa villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == booking.VillaId);
            booking.TotalCost = villa.Price * booking.Nights;
            
            booking.BookingDate = DateTime.Now;
            booking.Status = StaticDetails.StatusApproved;

            _unitOfWork.BookingRepositoryUOW.Add(booking);
            _unitOfWork.Save();
            return RedirectToAction(nameof(BookingConfirmation), new { bookingId=booking.Id });
        }

        public IActionResult BookingConfirmation(int bookingId)
        {
            return View(bookingId);
        }
    }
}
