using AgroPrice.Services.WholeSaleMarket.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.PointOfSale.Models;

namespace AgroPrice.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Domain.WholeSaleMarket.WholeSaleMarket, WholeSaleMarketModel>();
            CreateMap<WholeSaleMarketModel, Domain.Domain.WholeSaleMarket.WholeSaleMarket>();

            CreateMap<Domain.Domain.WholeSaleMarket.PointOfSale, PointOfSaleModel>();
            CreateMap<PointOfSaleModel, Domain.Domain.WholeSaleMarket.PointOfSale>();
        }
    }
}
