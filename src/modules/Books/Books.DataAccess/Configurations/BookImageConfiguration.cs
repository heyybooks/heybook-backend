using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Books.Entity.Concrete;
using Core.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccess.Configurations
{
    public class BookImageConfiguration : EntityTypeConfigurationBase<BookImage>
    {
        public override void Configure(EntityTypeBuilder<BookImage> builder)
        {
            builder.HasKey(bi => bi.BookImageId);
            builder.Property(bi => bi.ImageUrl).IsRequired(false);
            builder.Property(bi => bi.UploadedDate).HasDefaultValueSql("NOW()");
        }
    }
}
