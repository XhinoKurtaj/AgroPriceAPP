using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;
using AgroPrice.Core.Data.Mapping;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPrice.Data.Mapping.Product
{
    public class ProductDetailsMap : BaseEntityTypeConfiguration<ProductDetails>
    {
        public override void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            builder.MapDefaults();

            builder.Property(mapping => mapping.Price).IsRequired();
            builder.Property(mapping => mapping.Quantity).IsRequired();
            builder.Property(mapping => mapping.ModificationDate).IsRequired();

            builder.HasOne(model => model.Product)
                .WithMany(model => model.ProductDetails)
                .HasForeignKey(model => model.ProductId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
