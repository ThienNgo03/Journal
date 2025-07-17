using Journal.Databases.Campaigns;
using Journal.Journeys;
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
    options.PublishMessage<Journal.Journeys.Post.Messager.Message>().ToLocalQueue("journey-post");
    options.PublishMessage<Journal.Journeys.Update.Messager.Message>().ToLocalQueue("journey-update");

    options.PublishMessage<Journal.Users.Delete.Messager.Message>().ToLocalQueue("user-delete");
    options.PublishMessage<Journal.Users.Post.Messager.Message>().ToLocalQueue("user-post");
    options.PublishMessage<Journal.Users.Update.Messager.Message>().ToLocalQueue("user-update");

    options.PublishMessage<Journal.Notes.Delete.Messager.Message>().ToLocalQueue("note-delete");
    options.PublishMessage<Journal.Notes.Post.Messager.Message>().ToLocalQueue("note-post");
    options.PublishMessage<Journal.Notes.Update.Messager.Message>().ToLocalQueue("note-update");
});
builder.Services.AddJourneys(builder.Configuration);
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
