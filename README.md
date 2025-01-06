
## Calendar Of Events ✏️🗓️

Calendar of Events, allows us to manage events or reminders within a calendar, it has the function of showing, creating, updating and deleting events in a simple way that in turn, through SignalR, notifies us when an event has started.

## Table of Contents
- [Installation](#installation)
- [Use](#use)
- [Characteristics](#characteristics)
- [Author](#author)

## installation
1.Clone this repository:

    git clone https://github.com/santvallejos/CalendarOfEvents-Backend.git

2.Specify the connection string:
Place the connection string in your project's appsettings.json, for example:

    "ConnectionStrings": {
        "DefaultConnetion": "Server=localhost,1433;Database=CalendarOfEvents;User Id=sa;Password=########;TrustServerCertificate=True"
    }

3.Also in the CalendarOfEventsDbContextFactory, for example:

    optionsBuilder.UseSqlServer("Server=localhost,1433;Database=CalendarOfEvents;User Id=sa;Password=########;TrustServerCertificate=True");

4.Indicate the origin from which we want to make the requests, for example:

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

5.Update the DB

    dotnet ef database update

6.Running the API:
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