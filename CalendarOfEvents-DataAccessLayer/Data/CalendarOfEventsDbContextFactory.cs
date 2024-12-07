using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CalendarOfEvents_DataAccessLayer.Data
{
    public class CalendarOfEventsDbContextFactory : IDesignTimeDbContextFactory<CalendarOfEventsDbContext>
    {
        public CalendarOfEventsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CalendarOfEventsDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=CalendarOfEvents;User Id=sa;Password=Santi14183102*;TrustServerCertificate=True");

            return new CalendarOfEventsDbContext(optionsBuilder.Options);
        }
    }
}
