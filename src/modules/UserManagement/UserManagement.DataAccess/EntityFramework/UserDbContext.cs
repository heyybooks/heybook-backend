using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Entity.Concrete;



namespace UserManagement.DataAccess.EntityFramework
{
    public class UserDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    // PostgreSQL bağlantı dizesi
                    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hey-books;Username=postgres;Password=admin123");
                }
            }
        public DbSet<User> User { get; set; }   

        
    }
}