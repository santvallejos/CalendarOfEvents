using System.ComponentModel;
using Newtonsoft.Json.Converters;

namespace CalendarOfEvents_WebAPI.Infrastructure.DTO
{
    public class PostEventDto
    {
        public string Title { get; set; }
        public string EventDate { get; set; }
        public int EventHour { get; set; }
        public int EventMinute { get; set; }
        public string Description { get; set; }
    }
}