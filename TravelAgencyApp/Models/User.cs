namespace TravelAgencyApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // or hashed
        public string Role { get; set; } // "admin", "staff"
    }
}
