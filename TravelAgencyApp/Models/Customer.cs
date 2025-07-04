using System.ComponentModel.DataAnnotations;

namespace TravelAgencyApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
