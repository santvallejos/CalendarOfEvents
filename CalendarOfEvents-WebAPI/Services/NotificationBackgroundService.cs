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
    private readonly Timer _timer;
    private bool _isRunning = false;

    public NotificationBackgroundService(IHubContext<EventNotificationsHub> context, IServiceProvider serviceProvider)
    {
        _hubContext = context;
        _serviceProvider = serviceProvider;
        _timer = new Timer (CheckAndScheduleNotifications, null, Timeout.Infinite, Timeout.Infinite);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingtoken)
    {
        //Configura el Timer para ejecutarse inmediatamente
        _timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(1)); //Analizar cada minuto
        return Task.CompletedTask;
    }

    private void CheckAndScheduleNotifications(object? state)
    {
        // Evitar que el temporizador se ejecute si ya está procesando
        if (_isRunning) return;

        try{
            _isRunning = true;

            //Lógica para verificar y enviar notificaciones
            using (var scope = _serviceProvider.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetRequiredService<EventService>();
                var events = eventService.GetUpcomingEvents();

                foreach (var evt in events)
                {
                    var delay = evt.EventDate - DateTime.Now;
                    if(delay > TimeSpan.Zero)
                    {
                        Task.Delay(delay).ContinueWith(async _ =>
                        {
                            await NotifyEvent(evt);
                            eventService.MarkAsNotified(evt);
                        });
                    }
                }
            }
        }
        finally{
            _isRunning = false;
        }
    }

    private async Task NotifyEvent(Event evt)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", evt.Title, evt.EventDate, evt.FinishEventDate);
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}