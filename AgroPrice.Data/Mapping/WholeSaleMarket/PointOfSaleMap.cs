using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core.Data.Mapping;
using AgroPrice.Core.Extensions;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPrice.Data.Mapping.WholeSaleMarket
{
    public class PointOfSaleMap : BaseEntityTypeConfiguration<PointOfSale>
    {
        public override void Configure(EntityTypeBuilder<PointOfSale> builder)
        {
            builder.MapDefaults();

            builder.Property(model => model.Description).HasMaxLength(5000).IsRequired(); ;

            builder.HasOne(model => model.WholeSaleMarket)
                .WithMany(model => model.PointOfSales)
                .HasForeignKey(model => model.WholeSaleMarketId)
                .IsRequired();

            builder.HasOne(mapping => mapping.User)
                .WithOne(user => user.PointOfSale)
                .HasForeignKey<PointOfSale>(mapping => mapping.UserId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
