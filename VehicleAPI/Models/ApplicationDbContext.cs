using Microsoft.EntityFrameworkCore;

namespace VehicleAPI.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
