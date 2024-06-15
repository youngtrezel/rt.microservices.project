using Commercial.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Commercial.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Plate> Plates { get; set; }


    }
}
