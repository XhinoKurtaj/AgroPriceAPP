﻿using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core.Data.Mapping;
using AgroPrice.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AgroPrice.Data.Mapping.WholeSaleMarket
{
    public class WholeSaleMarketMap : BaseEntityTypeConfiguration<Domain.Domain.WholeSaleMarket.WholeSaleMarket>
    {
        public override void Configure(EntityTypeBuilder<Domain.Domain.WholeSaleMarket.WholeSaleMarket> builder)
        {
            builder.MapDefaults();

            builder.Property(model => model.Name).HasMaxLength(256).IsRequired();
            builder.Property(model => model.Address).HasMaxLength(5000).IsRequired();
            
            base.Configure(builder);
        }
    }
}