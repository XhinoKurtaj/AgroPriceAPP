using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Functional;
using AgroPrice.Services.PointOfSale.Models;

namespace AgroPrice.Services.PointOfSale
{
    public interface IPointOfSaleService
    {
        //return the list with all pointOfSales
        Task<List<PointOfSaleModel>> GetAllPointOfSale();

        //return pointOfSaleDetails model refering to specific id
        Task<PointOfSaleDetailsModel> PointOfSaleDetails(Guid id);

        // create pointOfSale and a seller 
        Task<Result> CreatePointOfSaleAndSeller(CreateSellerWithPointOfSaleModel model);

        // update pointOfSale and a seller
        Task<Result> UpdatePointOfSaleAndSeller(UpdateSellerWithPointOfSaleModel model);


        //return PointOfSale entity to model
        Task<UpdateSellerWithPointOfSaleModel> ReturnEntityToModel(
            Domain.Domain.WholeSaleMarket.PointOfSale entity);

        //Delete point Of Sale
        Task<Result> DeletePointOfSale(Guid id);
    }
}
