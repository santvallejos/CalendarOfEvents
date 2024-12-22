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
    public List<Event> GetStartEvents()
    {
        //Parsear la fecha
        DateTime date = DateTime.Now;
        string dateStr = date.ToString("dd-MM-yyyy");

        //Por cada evento que tenga Fecha, hora y minuto igual a la actual y que no haya sido notificado
        //Se agrega a la lista de eventos empezados que se notificaran 
        return _context.Events
            .Where(e => e.EventDate == dateStr && e.EventHour == DateTime.Now.Hour && e.EventMinute == DateTime.Now.Minute && e.SendNotification == false)
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