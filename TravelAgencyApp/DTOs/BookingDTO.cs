using TravelAgencyApp.Models;

namespace TravelAgencyApp.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }

        public int TripId { get; set; }
        public int CustomerId { get; set; }

        public string TripName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public BookingStatus Status { get; set; }
    }
}
