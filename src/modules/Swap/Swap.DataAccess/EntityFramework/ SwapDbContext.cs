using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Entity.Concrete;
using Books.Entity.Concrete; 
using Swap.Entity.Concrete;


using Microsoft.Extensions.Configuration;

namespace Swap.DataAccess.EntityFramework
{
    public class  SwapDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    // PostgreSQL bağlantı dizesi
                    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hey-books;Username=postgres;Password=admin123");
                }
            }
        public DbSet<SwapRequest> SwapRequests { get; set; }
        public DbSet<SwapRating> SwapRatings { get; set; }
    }
}