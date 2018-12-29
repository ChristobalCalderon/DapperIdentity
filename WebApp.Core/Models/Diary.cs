using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Models
{
    public class Diary
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Entry { get; set; }
        public string Subject { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
