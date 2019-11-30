using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Services.PointOfSale.Models;

namespace AgroPrice.Services.PointOfSale
{
    public interface IPointOfSaleService
    {
        Task<List<PointOfSaleModel>> GetAllPointOfSale();

        Task<PointOfSaleDetailsModel> PointOfSaleDetails(Guid id);
    }
}
