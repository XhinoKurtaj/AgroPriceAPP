using System;
using System.Collections.Generic;
using System.Text;
using AgroPrice.Core;

namespace AgroPrice.Services.Account.Models
{
    public class ChangePasswordModel : BaseModel
    {
        public string Password { get; set; }

        public string ConfirmationPassword { get; set; }
    }
}
