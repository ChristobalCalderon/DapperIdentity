using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Models
{
    public class Attendant
    {
        public string UserId { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public bool Attend { get; set; }
    }
}
