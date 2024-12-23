using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Books.Entity.Concrete;
using Core.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess.Configurations
{
    public class BookConfiguration : EntityTypeConfigurationBase<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);
            builder.Property(b => b.BookName).IsRequired().HasMaxLength(200);
            builder.HasMany(b => b.BookImages)
                   .WithOne(bi => bi.Book)
                   .HasForeignKey(bi => bi.BookId);
                   

            // Ortak yapılandırma:
            ConfigureBaseProperties(builder);
        }
    }
}
