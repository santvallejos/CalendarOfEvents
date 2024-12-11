using CalendarOfEvents_DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(); //Añadir el servicio de SignalR

//Contexto a mi base de datos
builder.Services.AddDbContext<CalendarOfEventsDbContext>
    (
     options => options.UseSqlServer(
         builder.Configuration.GetConnectionString("DefaultConnetion")
         )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting(); //Uso de rutas 

app.MapControllers();
app.MapHub<NotificationEventHub>("/Notification"); //URL del Hub

app.Run();
