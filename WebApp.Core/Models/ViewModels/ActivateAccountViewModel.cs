using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Models.ViewModels
{
    public class ActivateAccountViewModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
