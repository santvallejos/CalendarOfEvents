using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CalendarOfEvents_DataAccessLayer;
using CalendarOfEvents_DataAccessLayer.Data;
using CalendarOfEvents_DataAccessLayer.Models;

public class EventService
{
    //Contexto a la DB
    private readonly CalendarOfEventsDbContext _context;
    public EventService(CalendarOfEventsDbContext context)
    {
        _context = context;
    }

    //Enlistar las notificaciones
    public List<Event> GetUpcomingEvents()
    {
        var now = DateTime.Now;
        return _context.Events
                    .Where(e => e.EventDate > now &&
                                e.EventDate <= now.AddHours(1) &&
                                e.SendNotification == false)
                    .ToList();
    }

    //La notificacion fue enviada
    public void MarkAsNotified(Event evt)
    {
        evt.SendNotification = true;
        _context.Events.Update(evt);
        _context.SaveChanges();
    }
}