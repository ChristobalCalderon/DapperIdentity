using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Enums;

namespace WebApp.Core.Models
{
    public class Event
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
