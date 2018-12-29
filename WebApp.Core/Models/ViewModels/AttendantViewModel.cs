using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Core.Models.ViewModels
{
    public class AttendantViewModel
    {
        [Required]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [Required]
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
        [Required]
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [JsonProperty(PropertyName = "attend")]
        public bool Attend { get; set; }
    }
}
