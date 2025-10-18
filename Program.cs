using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Infrastructure.Data;
using Theater_Management_BE.src.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Theater_Management_BE.src.Infrastructure.Utils;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Connect to database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    });
builder.Services.AddScoped<AuthTokenUtil>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new AuthTokenUtil(config);
});
builder.Services.AddScoped<Theater_Management_BE.src.Infrastructure.Utils.EmailValidator>();
builder.Services.AddScoped<Theater_Management_BE.src.Application.Interfaces.IUserRepository, Theater_Management_BE.src.Infrastructure.Repositories.UserRepository>();
builder.Services.AddScoped<Theater_Management_BE.src.Application.Services.UserService>();

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
