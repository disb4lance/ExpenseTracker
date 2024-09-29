using ExpenseTracker.Extentions;
using NLog;

var builder = WebApplication.CreateBuilder(args);


LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));



builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));


// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
