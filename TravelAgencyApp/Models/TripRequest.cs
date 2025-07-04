namespace TravelAgencyApp.Models
{
    public class TripRequest
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
        public string RequestedByUserId { get; set; }
        public bool IsApproved { get; set; }
    }
}
