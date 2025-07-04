using TravelAgencyApp.Models;

namespace TravelAgencyApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Trips.Any())
            {
                var trips = new Trip[]
                {
        new Trip { Name = "Beach Paradise", Destination = "Bali", Price = 1200, StartDate = DateTime.Now.AddDays(15), EndDate = DateTime.Now.AddDays(22) },
        new Trip { Name = "City Break", Destination = "Prague", Price = 500, StartDate = DateTime.Now.AddDays(5), EndDate = DateTime.Now.AddDays(8) }
                };

                context.Trips.AddRange(trips);
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                var customers = new Customer[]
                {
        new Customer { FullName = "John Doe", Email = "john@example.com" },
        new Customer { FullName = "Jane Smith", Email = "jane@example.com" }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            var trip1 = context.Trips.FirstOrDefault(); // Just get any trip
            var trip2 = context.Trips.Skip(1).FirstOrDefault(); // Get second trip

            var customer1 = context.Customers.FirstOrDefault();
            var customer2 = context.Customers.Skip(1).FirstOrDefault();


            if (!context.Bookings.Any() && trip1 != null && trip2 != null && customer1 != null && customer2 != null)
            {
                var bookings = new Booking[]
                {
        new Booking { BookingDate = DateTime.Now, TripId = trip1.TripId, CustomerId = customer1.CustomerId },
        new Booking { BookingDate = DateTime.Now, TripId = trip2.TripId, CustomerId = customer2.CustomerId }
                };

                context.Bookings.AddRange(bookings);
                context.SaveChanges();
            }


        }
    }
}

