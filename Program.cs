using MessageService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

// Добавляем контекст базы данных с использованием PostgreSQL
builder.Services.AddDbContext<MessageDbContext>(options =>
	options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<MessageWebSocketHandler>(); 
builder.Services.AddHostedService<DatabaseInitializer>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		policy =>
		{
			policy.AllowAnyOrigin()
				  .AllowAnyHeader()
				  .AllowAnyMethod();
		});
});

builder.WebHost.UseUrls("http://*:7223"); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Message Service API V1");
	});
}
// Enable CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

// Используем маршрутизацию
app.UseRouting();

// Используем WebSockets
app.UseWebSockets();

// Используем авторизацию (если есть)
app.UseAuthorization();

// Определяем конечные точки (Endpoints)
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();

	// Настройка WebSocket маршрута
	endpoints.Map("/ws", async context =>
	{
		var webSocketHandler = context.RequestServices.GetService<MessageWebSocketHandler>();
		if (webSocketHandler != null)
		{
			await webSocketHandler.HandleWebSocketAsync(context);
		}
	});
});

app.Run();
