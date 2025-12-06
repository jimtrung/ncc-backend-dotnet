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

// Keest nối với CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Setting cho pool
var connectionStringBuilder = new Npgsql.NpgsqlConnectionStringBuilder(connectionString)
{
    MinPoolSize = 2,
    MaxPoolSize = 20, 
    ConnectionIdleLifetime = 300, 
    ConnectionPruningInterval = 10
};

// Mapping enum cho CSDL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        connectionStringBuilder.ConnectionString,
        npgsqlOptions =>
        {
            npgsqlOptions.MapEnum<Provider>("provider_type");
            npgsqlOptions.MapEnum<UserRole>("role_type");
            npgsqlOptions.MapEnum<MovieGenre>("movie_genre");
        });
});

// Thêm xác thực bằng JWTn
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

// Setting để json dùng snake_case và tự động chuyển giữ enum <-> string
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Tạo AuthTokenUtil
builder.Services.AddScoped<AuthTokenUtil>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new AuthTokenUtil(config);
});

// Tạo EmailValidator
builder.Services.AddScoped<EmailValidator>();

// Thêm các repository
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

// Thêm các service
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DirectorService>();
builder.Services.AddScoped<ActorService>();
builder.Services.AddScoped<MovieActorService>();

// Thêm xác thực
builder.Services.AddAuthorization();

var app = builder.Build();

// Làm nóng kết nối
// NOTE: 04/12/25 - Nếu không làm nóng kết nối trước thì request đầu tiên gửi sẽ mất khoảng 2s để
// kết nối + setting. Vậy thay vì để người dùng đợi 2s thì ta để server mất thêm 2s để chạy
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // Gọi một lệnh bất kì
        await dbContext.Movies.AnyAsync();
        Console.WriteLine("Database connection pool đã được khởi tạo");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Warning: Không thể khởi tạo connection pool: {ex.Message}"); 
    }
}

// Thêm middleware để xử lý exception
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Thêm các endpoint
app.MapControllers();

app.Run();
