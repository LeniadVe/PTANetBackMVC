using AdministrationAPI.Context;
using AdministrationAPI.Interfaces;
using AdministrationAPI.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFeeService, FeeService>();
builder.Services.AddHttpClient<IFeeService, FeeService>();
builder.Services.AddScoped<IDBFeeService, DBFeeService>();

Env.Load();
var server = Environment.GetEnvironmentVariable("CONNECTIONSECRETS_SERVER");
var database = Environment.GetEnvironmentVariable("CONNECTIONSECRETS_DATABASE");
var user = Environment.GetEnvironmentVariable("CONNECTIONSECRETS_USER");
var password = Environment.GetEnvironmentVariable("CONNECTIONSECRETS_PASSWORD");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                                                .Replace("{SERVER}", server)
                                                .Replace("{DATABASE}", database)
                                                .Replace("{USER}", user)
                                                .Replace("{PASSWORD}", password)));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

