using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core.Data.Mapping;
using AgroPrice.Core.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPrice.Data.Mapping.Product
{
    public class ProductMap : BaseEntityTypeConfiguration<Domain.Domain.Product.Product>
    {
        public override void Configure(EntityTypeBuilder<Domain.Domain.Product.Product> builder)
        {
            builder.MapDefaults();

            builder.Property(model => model.Name).HasMaxLength(256);
            builder.Property(model => model.Quantity).IsRequired();
            builder.Property(model => model.Origin).IsRequired().HasMaxLength(256);
            builder.Property(model => model.SupplyDate).IsRequired();

            builder.HasOne(model => model.PointOfSale)
                .WithMany(model => model.Products)
                .HasForeignKey(model => model.PointOfSaleId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
