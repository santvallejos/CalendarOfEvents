using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.SignalR;

public class HangfireJobs
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<EventNotificationsHub> _hubContext;

    public HangfireJobs(IServiceProvider serviceProvider, IHubContext<EventNotificationsHub> hubContext)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }

    // Método recurrente que se ejecuta cada minuto para revisar los eventos próximos
    public async Task ProcessNotifications()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var eventService = scope.ServiceProvider.GetRequiredService<EventService>();
            var events = eventService.GetUpcomingEvents();

            foreach (var evt in events)
            {
                var delay = evt.EventDate - DateTime.Now;
                if (delay > TimeSpan.Zero)
                {
                    // Se programa un job diferido pasando el Guid del evento
                    BackgroundJob.Schedule<HangfireJobs>(
                        job => job.SendNotification(evt.Id),
                        delay);
                }
            }
        }
    }

    // Método que envía la notificación y marca el evento como notificado
    public async Task SendNotification(Guid eventId)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var eventService = scope.ServiceProvider.GetRequiredService<EventService>();
            var evt = eventService.GetEventById(eventId);
            if (evt != null && !evt.SendNotification)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", evt.Title, evt.EventDate, evt.FinishEventDate);
                eventService.MarkAsNotified(evt);
            }
        }
    }
}