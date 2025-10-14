using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Infrastructure.Data;
using Theater_Management_BE.src.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Connect to database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    });
builder.Services.AddScoped<Theater_Management_BE.src.Application.Interfaces.IUserRepository, Theater_Management_BE.src.Infrastructure.Repositories.UserRepository>();
builder.Services.AddScoped<Theater_Management_BE.src.Application.Services.UserService>();

// Learn more about configuring Sagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
