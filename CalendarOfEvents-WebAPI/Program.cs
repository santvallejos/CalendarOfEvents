using CalendarOfEvents_DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(); //Añadir el servicio de SignalR
builder.Services.AddScoped<EventService>();//Servicio de enventos
builder.Services.AddHostedService<NotificationBackgroundService>();//Servicio en segundo plano de las notificaciones

//Contexto a mi base de datos
builder.Services.AddDbContext<CalendarOfEventsDbContext>
    (
     options => options.UseSqlServer(
         builder.Configuration.GetConnectionString("DefaultConnetion")
         )
    );

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Ajusta el origen seg�n tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true) // Permitir todas las solicitudes de origen
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost4200"); // Aplicar la pol�tica de CORS

app.UseAuthorization();
app.UseRouting(); //Uso de rutas 

app.MapControllers();
app.MapHub<EventNotificationsHub>("/Notifications"); //URL del Hub

app.Run();