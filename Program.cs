using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Infrastructure.Data;
using Theater_Management_BE.src.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Theater_Management_BE.src.Infrastructure.Utils;
using System.IdentityModel.Tokens.Jwt;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Infrastructure.Repositories;
using Theater_Management_BE.src.Application.Services;
using Theater_Management_BE.src.Domain.Repositories;
using Theater_Management_BE.src.Domain.Enums;

var builder = WebApplication.CreateBuilder(args);

// Connect to database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.MapEnum<Provider>("provider_type");
            npgsqlOptions.MapEnum<UserRole>("role_type");
            // Gender is stored as string in entities; no enum mapping needed
            npgsqlOptions.MapEnum<MovieGenre>("movie_genre");
        });
});

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),

        NameClaimType = JwtRegisteredClaimNames.Sub
    };
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddScoped<AuthTokenUtil>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new AuthTokenUtil(config);
});
builder.Services.AddScoped<EmailValidator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IAuditoriumRepository, AuditoriumRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IMovieActorRepository, MovieActorRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DirectorService>();
builder.Services.AddScoped<ActorService>();
builder.Services.AddScoped<MovieActorService>();


// Learn more about configuring Sagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
