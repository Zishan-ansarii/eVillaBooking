using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVillaBooking.Domain.Entities
{
    public class Booking
    {
        // Primary key for the Booking entity
        public int Id { get; set; }

        //Navigation property for the user who made the booking
        public ApplicationUser User { get; set; }

        // Foreign key for the User
        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }

        // Navigation property for the booked villa
        public Villa Villa { get; set; }

        // Foreign key for villa
        [Required, ForeignKey(nameof(Villa))]
        public int VillaId { get; set; }

        // Name of the person making the booking
        [Required]
        public string Name { get; set; }

        // Email of the person making the booking
        [Required]
        public string Email { get; set; }

        // optional phone number of the person making the booking
        public string? Phone { get; set; }

        // total cost of the booking
        [Required]
        public double  TotalCost { get; set; }

        // Number of nights the villa is booked for
        public int Nights { get; set; }

        // Status of the booking (e.g., Confirmed, Pending, Canceled)
        public string? Status { get; set; }

        // Date when booking was made
        public DateTime BookingDate { get; set; }

        // Date of Check-in
        [Required]
        public DateOnly CheckInDate { get; set; }

        // Date of check-out
        [Required]
        public DateOnly CheckOutDate { get; set; }

        // Indicating whether the payment was successfull (default: false)
        public bool IsPaymentSuccessful { get; set; } = false;

        // Date of successful payment
        public DateTime PaymentDate { get; set; }

        // Strip session ID for tracking payments
        public string? StripSessionId { get; set; }

        // Strip payment intent ID for tracking transaction
        public string? StripPaymentIntentId { get; set; }

        // Actual check-in date (useful for tracking any changes to the planned check-in)
        public DateOnly ActualCheckInDate { get; set; }

        // Actual check-out date (useful for tracking any changes to the planned check-out
        public DateOnly ActualCheckOutDate { get; set; }

        // the specific villa number assigned to the booking
        public int VillaNumber { get; set; }
    }
}
