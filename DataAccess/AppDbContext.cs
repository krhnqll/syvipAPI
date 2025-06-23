using Microsoft.EntityFrameworkCore;
using Entities.All.Models.Admin;
using System.Collections.Generic;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
