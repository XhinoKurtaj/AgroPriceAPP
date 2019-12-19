using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPrice.Core.Data;
using AgroPrice.Domain.Domain.User;
using AgroPrice.Functional;
using AgroPrice.Services.Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AgroPrice.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Domain.Domain.Product.Product> _products;
        private readonly IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> _pointOfSale;

        public MailService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IDbContext dbContext, IRepository<Domain.Domain.Product.Product> products, IRepository<Domain.Domain.WholeSaleMarket.PointOfSale> pointOfSale)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
            _products = products;
            _pointOfSale = pointOfSale;
        }

        public async Task<Result> CheckoutMessages(List<Item> products)
        {
            var users = await _userManager.GetUsersInRoleAsync("Seller");
            var sellers = new List<User>();
            foreach (var seller in users)
            {
                sellers.Add((User)seller);
            }
            var results = products.GroupBy(
                p => p.Product.PointOfSaleId,
                p => p.Product,
                (key, g) => new { PointOfSaleId = key, Products = g.ToList()});
            foreach (var group in results)
            {
                string message = "";
                var seller = sellers.FirstOrDefault(x => x.PointOfSaleId == group.PointOfSaleId);
                var content = new Content();
                content.Type = "text/html";
                content.Value = "";
                foreach (var product in group.Products)
                {
                    content.Value += string.Format(@"Produkti-> {0} | Quantity-> {1} <br/>", product.Name, product.Quantity );
                }

                await SendMessage(seller.UserName, seller.Email, content);
            }
            return Result.Ok();
        }

        public async Task<Result> SendMessage(string firstName, string email, Content message)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_configuration.GetSection("SenderEmail").Value,
                _configuration.GetSection("SenderName").Value);
            var to = new EmailAddress("ton.driza@gmail.com", firstName);
            var subject = "AgroPrice.com";
            var content = message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, content.Type, content.Value);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode.ToString().ToLower() == "accepted")
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Sending email failed");
            }
        }
    }
}
