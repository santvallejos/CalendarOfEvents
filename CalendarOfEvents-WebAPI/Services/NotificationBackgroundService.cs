using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CalendarOfEvents_DataAccessLayer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

public class NotificationBackgroundService : BackgroundService
{
    //Contexto a mi Hub y a la interfas de serivio a implementar
    private readonly IHubContext<EventNotificationsHub> _hubContext;
    private readonly IServiceProvider _serviceProvider;

    public NotificationBackgroundService(IHubContext<EventNotificationsHub> context, IServiceProvider serviceProvider)
    {
        _hubContext = context;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingtoken)
    {
        while(!stoppingtoken.IsCancellationRequested) //Mientras se tenga conexion
        {
            using(var scope = _serviceProvider.CreateScope()) //se crea un scope
            {
                var eventService = scope.ServiceProvider.GetRequiredService<EventService>(); //invocar al servicio de eventos
                var startEvents = eventService.GetStartEvents(); //Enlistar todos los evetnos

                //Notificar cada evento
                foreach (var evt in startEvents)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", 
                        evt.Title, evt.Description, evt.EventDate, evt.EventHour, evt.EventMinute);

                    eventService.MarkAsNotified(evt);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingtoken);
        }
    }
}