using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CalendarOfEvents_DataAccessLayer.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime FinishEventDate { get; set; }
        public string Description { get; set; }
        public bool SendNotification { get; set; } = false;
    }
}
