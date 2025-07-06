namespace TravelAgencyApp.Models
{
    public enum BookingStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
    }
}
