using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        protected void ConfigureBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property("CreatedDate").HasDefaultValueSql("NOW()");
        }
    }
}
