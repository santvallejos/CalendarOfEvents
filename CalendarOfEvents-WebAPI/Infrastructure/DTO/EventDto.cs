namespace CalendarOfEvents_WebAPI.Infrastructure.DTO
{
    public class PostEventDto
    {
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
    }
}