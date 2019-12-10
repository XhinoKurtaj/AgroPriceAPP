using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Functional;
using AgroPrice.Services.Product.Models;

namespace AgroPrice.Services.Mail
{
    public interface IMailService
    {
        Task<Result> CheckoutMessages(List<Item> products);
    }
}
