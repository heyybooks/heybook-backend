using Books.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.DataAccess.EntityFramework
{
    public class MessageContext : DbContext
    {
        // Veritabanı bağlantı ayarları burada yapılır
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    // PostgreSQL bağlantı dizesi
                    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hey-books;Username=postgres;Password=admin123");
                }
            }

        // DbSet tanımları (örneğin Book ve BookImage tabloları)
        public DbSet<Message> Messages { get; set; }
       
    }
}
        