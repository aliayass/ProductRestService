using Microsoft.EntityFrameworkCore;
using ProductSOAPnew.Models;
using System.Collections.Generic;

namespace ProductSOAPnew.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
