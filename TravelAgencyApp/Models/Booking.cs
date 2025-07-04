namespace TravelAgencyApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
