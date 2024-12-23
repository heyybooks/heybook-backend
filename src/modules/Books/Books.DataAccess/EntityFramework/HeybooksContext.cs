using Books.DataAccess.Configurations;
using Books.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class HeybooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        // Veritabanı bağlantı ayarları burada yapılır
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // PostgreSQL bağlantı dizesi
                optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = hey-books; Username = postgres; Password = admin123");


            }
        }
        // DbSet tanımları (örneğin Book ve BookImage tabloları)



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Book>()
            //    .HasMany(b => b.BookImages)
            //    .WithOne(bi => bi.Book)
            //    .HasForeignKey(bi => bi.BookId);


            // modelBuilder.ApplyConfiguration(new BookConfiguration());
            // modelBuilder.ApplyConfiguration(new BookImageConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeybooksContext).Assembly); // otomatik olarak tum configurationlari ceker
            base.OnModelCreating(modelBuilder);
        }


    }
}
