using Microsoft.EntityFrameworkCore;
using Publisher;
using Publisher.Data;
using Publisher.Interfaces;
using Publisher.Services;
using Test.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSqlLite")!;
builder.Services.AddDbContext<AppDataContext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// add global exception handling middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

// initialize db
await app.Initialize();

app.Run();
