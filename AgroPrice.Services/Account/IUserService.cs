using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Domain.Domain.User;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Services.Account
{
    public interface IUserService
    {
        //return all sellers
        Task<IList<IdentityUser>> GetAllSellers();

        //return all buyers
        Task<IList<IdentityUser>> GetAllBuyers();

        //return all priceOperators
        Task<IList<IdentityUser>> GetAllPriceOperators();
    }
}
