using Marketing.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketing.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Plate> Plates { get; set; }


    }
}
