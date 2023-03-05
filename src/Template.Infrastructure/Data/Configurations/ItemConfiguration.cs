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
            builder.OwnsMany(i => i.Properties, o =>
            {
                o.ToTable("itemproperty");
                //hidden foreign key field
                o.Property("ItemId").HasColumnName("itemid");
                o.HasKey(i => i.Id);
                o.Property(i => i.Id).HasColumnName("id");
                o.Property(i => i.Property).HasColumnName("id").HasMaxLength(100);
            });
        }
    }
}

