using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Core.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [JsonProperty(PropertyName = "fName")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "lName")]
        public string LastName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "personalIdentityNumber")]
        public string PersonalIdentityNumber { get; set; }

        [Required]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [JsonProperty(PropertyName = "confirmedPassword")]
        public string ConfirmPassword { get; set; }
    }
}
