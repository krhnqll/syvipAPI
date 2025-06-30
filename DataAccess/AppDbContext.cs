using Microsoft.EntityFrameworkCore;
using Entities.All;
using System.Collections.Generic;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }

    }
}
