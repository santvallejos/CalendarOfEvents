using System;
using System.Linq;
using System.Collections.Generic;
using CalendarOfEvents_DataAccessLayer.Data;
using CalendarOfEvents_DataAccessLayer.Models;

public class EventService
{
    private readonly CalendarOfEventsDbContext _context;
    public EventService(CalendarOfEventsDbContext context)
    {
        _context = context;
    }

    // Lista de eventos que ocurrirán en la próxima hora y que aún no han sido notificados
    public List<Event> GetUpcomingEvents()
    {
        var now = DateTime.Now;
        return _context.Events
                    .Where(e => e.EventDate > now &&
                                e.EventDate <= now.AddHours(1) &&
                                e.SendNotification == false)
                    .ToList();
    }

    // Marca un evento como notificado
    public void MarkAsNotified(Event evt)
    {
        evt.SendNotification = true;
        _context.Events.Update(evt);
        _context.SaveChanges();
    }

    // Obtiene un evento por su Guid
    public Event GetEventById(Guid id)
    {
        return _context.Events.FirstOrDefault(e => e.Id == id);
    }
}