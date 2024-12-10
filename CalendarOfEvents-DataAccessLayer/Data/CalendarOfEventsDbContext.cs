using CalendarOfEvents_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarOfEvents_DataAccessLayer.Data
{
    public class CalendarOfEventsDbContext : DbContext
    {
        public CalendarOfEventsDbContext(DbContextOptions<CalendarOfEventsDbContext> options) : base(options)
        {
        }

        //Entidad
        public DbSet<Event> Events { get; set; }

        //Modelo a crear
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //Configuracion de la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
