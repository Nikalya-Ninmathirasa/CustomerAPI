using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data
{
    public class CusDbContext : DbContext
    {

        public CusDbContext(DbContextOptions<CusDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
    }
}
