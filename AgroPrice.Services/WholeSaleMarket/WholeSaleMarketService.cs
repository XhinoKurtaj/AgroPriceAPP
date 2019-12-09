﻿using AgroPrice.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Domain.Domain.WholeSaleMarket;
using AgroPrice.Services.WholeSaleMarket.Models;
using System.Threading.Tasks;
using System.Linq;
using AgroPrice.Functional;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AgroPrice.Services.WholeSaleMarket
{
    public class WholeSaleMarketService : IWholeSaleMarketService
    {
        private readonly IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> _wholeSaleMarket;
        private readonly IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> _pointOfSale;
        private readonly IDbContext _context;

        public WholeSaleMarketService(IRepository<Domain.Domain.WholeSaleMarket.WholeSaleMarket> wholeSaleMarket, IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> pointOfSale, IDbContext context)
        {
            _wholeSaleMarket = wholeSaleMarket;
            _pointOfSale = pointOfSale;
            _context = context;
        }

        public async Task<List<WholeSaleMarketModel>> GetAllWholeSaleMarkets()
        {
            return await _wholeSaleMarket.TableNoTracking.ProjectTo<WholeSaleMarketModel>().ToListAsync();
        }

        /// <summary>
        /// delete whole sale market
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> DeleteWholeSaleMarket(Guid id)
        {
            try
            {
                var entity = await _wholeSaleMarket.GetByIdAsync(id);
                var pointOfSales = entity.PointOfSales;
                _pointOfSale.AutoSaveChanges = false;
                _wholeSaleMarket.AutoSaveChanges = false;
                if(pointOfSales.Any())
                    await _pointOfSale.DeleteAsync(pointOfSales);
                await _wholeSaleMarket.DeleteAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Fail(null,"Error while deleting wholeSaleMarket");
            }
            return Result.Ok();
        }

        /// <summary>
        /// create new whole sale market 
        /// </summary>
        /// <returns></returns>
        public async Task<Result> CreateWholeSaleMarket(CreateWholeSaleMarketModel model)
        {
            try
            {
                var entity = new Domain.Domain.WholeSaleMarket.WholeSaleMarket
                {
                    Name = model.Name,
                    Address = model.Address,
                    ImageUrl = model.ImageUrl
                };
                await _wholeSaleMarket.InsertAsync(entity);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(null,"Error while creating new whole sale market!");
            }
        }

        /// <summary>
        /// update whole sale market with specific id 
        /// </summary>
        /// <returns></returns>
        public async Task<Result> UpdateWholeSaleMarket(UpdateWholeSaleMarketModel model)
        {
            try
            {
                var entity = await _wholeSaleMarket.GetByIdAsync(model.Id);

                entity.Name = model.Name;
                entity.Address = model.Address;
                entity.ImageUrl = model.ImageUrl;
            
                await _wholeSaleMarket.UpdateAsync(entity);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(null, "Error while creating new whole sale market!");
            }
        }
    }
}
