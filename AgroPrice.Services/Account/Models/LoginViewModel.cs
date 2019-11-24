using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.Account.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// Email property on login
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password property on login 
        /// </summary>
        public string Password { get; set; }
    }
}
