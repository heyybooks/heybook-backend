using Microsoft.EntityFrameworkCore;
using Notifications.Entity.Concrete;

namespace Notifications.DataAccess.EntityFramework
{
    public class NotificationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hey-books;Username=postgres;Password=admin123");
            }
        }

        public DbSet<Notification> Notifications { get; set; }
    }
}
