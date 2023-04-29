using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;
using Template.Core.Item;

namespace Template.Infrastructure.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("item");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("id");
            builder.Property(i => i.Name).HasColumnName("name").HasMaxLength(100);
            builder.Property(i => i.Description).HasColumnName("description").HasMaxLength(255);
            builder.OwnsMany(i => i.Properties, o =>
            {
                o.ToTable("itemproperty").WithOwner().HasForeignKey("itemid");
                o.HasKey("itemid");
                //o.Property("ItemId").HasColumnName("itemid");
                o.Property(i => i.Property).HasColumnName("property").HasMaxLength(100);
            });
        }
    }
}

