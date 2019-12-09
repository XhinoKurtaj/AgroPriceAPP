using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.PointOfSale.Models
{
    public class PointOfSaleDetailsModel
    {
        public PointOfSaleModel PointOfSaleModel { get; set; }

        public List<Models.Product> Products { get; set; }
    }
}
