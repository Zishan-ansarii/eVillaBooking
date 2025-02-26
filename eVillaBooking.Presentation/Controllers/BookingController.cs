using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eVillaBooking.Presentation.Controllers
{
    public class BookingController : Controller

    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult FinalizeBooking(DateOnly checkInDate, int nights, int villaId)
        {    
            Booking booking = new()
            {
                CheckInDate = checkInDate,
                Nights = nights,
                VillaId = villaId,
                CheckOutDate = checkInDate.AddDays(nights),
                Villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == villaId,includeProperties:"AmenityList"),
            };

            booking.TotalCost = booking.Villa.Price * nights;
            return View(booking);
        }
    }
}
