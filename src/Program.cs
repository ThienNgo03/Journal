using Journal.Databases.Campaigns;
using Microsoft.EntityFrameworkCore;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//đăng ký service 
builder.Services.AddDbContext <JournalDbContext>(x => x.UseSqlServer("Server=localhost;Database=JOURNAL;Trusted_Connection=True;TrustServerCertificate=True;"));
builder.Services.AddWolverine(options =>
{
    options.PublishMessage<Journal.Journeys.Delete.Messager.Message>().ToLocalQueue("journey-delete");
});
var app = builder.Build();

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
