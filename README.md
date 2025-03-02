
## Calendar Of Events ✏️🗓️

Calendar of Events, allows us to manage events or reminders within a calendar, it has the function of showing, creating, updating and deleting events in a simple way that in turn, through SignalR, notifies us when an event has started.

## Table of Contents
- [Project architecture](#architecture)
- [Installation](#installation)
- [Use](#use)
- [Characteristics](#characteristics)
- [Author](#author)

## architecture

    CalendarOfEvents-Backend/
    ├── CalendarOfEvents-BusinessLayer
    │   ├── Services
    │   │   └── EventService // Notifications logic
    │   └── CalendarOfEvents-BusinessLayer.csproj
    │
    ├── CalendarOfEvents-DataAccessLayer
    │   ├── Data
    │   │   ├── CalendarOfEventsDbContext
    │   │   └── CalendarOfEventsDbContextFactory
    │   ├── Migrations
    │   ├── Models
    │   │   └── Event
    │   └── CalendarOfEvents-DataAcessLayer.csproj
    │
    ├── CalendarOfEvents-WebAPI
    │   ├── Controller
    │   │   └── EventController
    │   ├── Hub
    │   │   └── EventNotificationsHub
    │   ├── Infrastructure
    │   │   └── Dto
    │   │       └── EventDto
    │   ├── Jobs
    │   │    └── NotificationJobs
    │   ├── Services
    │   │   └── NotificationsBackgroundService
    │   ├── appsettings.Development
    │   ├── appsettings
    │   └── CalendarOfEvents-WebAPI.csproj
    │
    ├── Program
    └── CalendarOfEvents.sln

## installation
1.Clone this repository:

    git clone https://github.com/santvallejos/CalendarOfEvents-Backend.git

2.Specify the connection string:<br/>
Create the appsettings.json and put the connection string of your project, for example:

    {
    "Logging": {
        "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
        }
        },
        "ConnectionStrings": {
            "DefaultConnetion": "Server=########;Database=CalendarOfEvents;User Id=sa;Password=########;TrustServerCertificate=True"
        }
    }

3.Indicate the origin from which we want to make the requests, for example:

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost4200", policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
    });

4.I create the DB and update it

    dotnet ef database update

5.Running the API: <br/>
You can run the API in the CalendarOfEvents-WebAPI folder with:

    dotnet run

## use

You can use this API to generate events with Id, Title, EventDate, FinishEventDate, Description and SendNotification, in order to show the user the events that are present.
In addition, with the implementation of SignalR, it allows you to evaluate an event when it is about to start in order to send a notification to the user.

## characteristics

- Events CRUD
- Event notification

## author

[![LinkedIn Follow](https://img.icons8.com/?size=50&id=447&format=png&color=000000)](https://www.linkedin.com/in/santiago-vallejos-97a933236/)
[![Github](https://img.icons8.com/?size=50&id=62856&format=png&color=000000)](https://github.com/santvallejos)
