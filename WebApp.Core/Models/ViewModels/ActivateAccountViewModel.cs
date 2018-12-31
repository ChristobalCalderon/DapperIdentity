using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Core.Models.ViewModels
{
    public class ActivateAccountViewModel
    {
        [Required]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [Required]
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}
