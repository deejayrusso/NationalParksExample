using Microsoft.EntityFrameworkCore;
using NationalParks.Models;

namespace NationalParks.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Guest> Guests { get; set; }

    }
}
