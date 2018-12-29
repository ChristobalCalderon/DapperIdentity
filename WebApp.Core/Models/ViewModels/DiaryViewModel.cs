using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Models.ViewModels
{
    public class DiaryViewModel
    {
        public int Id { get; set; }
        public string Entry { get; set; }
        public string Subject { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
