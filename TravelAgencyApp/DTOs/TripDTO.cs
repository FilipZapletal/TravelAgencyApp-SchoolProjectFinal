namespace TravelAgencyApp.DTOs
{
    public class TripDTO
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
    }

}
