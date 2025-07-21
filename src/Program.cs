using Journal.Databases;
using Journal.Wolverine;
using Journal.Journeys;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//đăng ký service 
builder.Services.AddDatabases(builder.Configuration);
builder.Services.AddWolverines(builder.Configuration);
builder.Services.AddJourneys(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
