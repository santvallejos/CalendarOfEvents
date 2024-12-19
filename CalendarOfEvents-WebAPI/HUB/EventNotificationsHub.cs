using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class EventNotificationsHub : Hub
{
    //Enviar una notificacion
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync(message, null);
    }
}