using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Services.Account.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int JobType { get; set; }

        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }
    }
}
