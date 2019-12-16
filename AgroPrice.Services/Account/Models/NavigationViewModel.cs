using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Domain.Domain.User;
using Microsoft.AspNetCore.Identity;

namespace AgroPrice.Services.Account.Models
{
    public class NavigationViewModel
    {
        public IdentityUser IdentityUser { get; set; }

        public User User { get; set; }
    }
}
