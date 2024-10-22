using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarOfEvents_DataAccessLayer.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool SendNotification { get; set; }
    }
}
