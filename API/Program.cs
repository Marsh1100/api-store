//using Infrastructure.Data;
using System.Reflection;
using API.Extension;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.AddAplicacionServices();

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

builder.Services.AddDbContext<FormulaContext>(optionBuilder =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var loggerFactory = services.GetRequiredService<ILoggerFactory>();
	try
	{
		var context = services.GetRequiredService<FormulaContext>();
		await context.Database.MigrateAsync();

	}
	catch (Exception ex)
	{
		var _logger = loggerFactory.CreateLogger<Program>();
		_logger.LogError(ex, "Ocurrio un error durante la migración");
	}
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
