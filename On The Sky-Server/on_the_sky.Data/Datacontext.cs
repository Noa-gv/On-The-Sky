
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using on_the_sky.core.entities;

namespace on_the_sky
{
    public class Datacontext : DbContext
    {
        public DbSet<Flight> FlightDB { get; set; }
        public DbSet<Travel> TravelsDB { get; set; }
        public DbSet<Places> PlacesDB { get; set; }
        public DbSet<Users> UsersDB { get; set; }

        private readonly IConfiguration _configuration;
        public Datacontext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

}
