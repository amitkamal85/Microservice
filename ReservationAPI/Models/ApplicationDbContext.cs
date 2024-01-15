using Microsoft.EntityFrameworkCore;

namespace ReservationAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
