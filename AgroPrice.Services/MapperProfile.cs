using AgroPrice.Services.WholeSaleMarket.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Services.PointOfSale.Models;
using AgroPrice.Services.Product.Models;

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

            CreateMap<Domain.Domain.Product.Product, ProductModel>();
            CreateMap<ProductModel, Domain.Domain.Product.Product>();

            CreateMap<Domain.Domain.Product.Product, ProductInCartModel>()
                .ForMember(m => m.PointOfSaleName, o => o.MapFrom(v => v.PointOfSale.Description));
            CreateMap<ProductInCartModel, Domain.Domain.Product.Product>();
        }
    }
}
