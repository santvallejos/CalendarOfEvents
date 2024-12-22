using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CalendarOfEvents_DataAccessLayer.Data
{
    public class CalendarOfEventsDbContextFactory : IDesignTimeDbContextFactory<CalendarOfEventsDbContext>
    {
        public CalendarOfEventsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CalendarOfEventsDbContext>();
            optionsBuilder.UseSqlServer("Data Source=SANTI\\SQLEXPRESS;Initial Catalog=CalendarOfEventsDatabase;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

            return new CalendarOfEventsDbContext(optionsBuilder.Options);
        }
    }
}
