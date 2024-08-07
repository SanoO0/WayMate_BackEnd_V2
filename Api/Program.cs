using System.Text;
using Application;
using Application.Services.TokenJWT;
using Application.UseCases.Authentication;
using Application.UseCases.Booking;
using Application.UseCases.Trip;
using Application.UseCases.Users.Admin;
using Application.UseCases.Users.Driver;
using Application.UseCases.Users.Passenger;
using Application.UseCases.Users.User;
using Infrastructure.Ef;
using Infrastructure.Ef.Authentication;
using Infrastructure.Ef.Booking;
using Infrastructure.Ef.Trip;
using Infrastructure.Ef.Users.Admin;
using Infrastructure.Ef.Users.Driver;
using Infrastructure.Ef.Users.Passenger;
using Infrastructure.Ef.Users.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Read Config Files
var configs = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.Development.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddDbContext<WaymateContext>(a => a.UseSqlServer(
    builder.Configuration.GetConnectionString("db"))
);

//Repository
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<PasswordHasher>();

//Token
builder.Services.AddScoped<TokenService>();

//Use Case Trip
builder.Services.AddScoped<UseCaseFetchAllTrip>();
builder.Services.AddScoped<UseCaseCreateTrip>();
builder.Services.AddScoped<UseCaseFetchTripById>();
builder.Services.AddScoped<UseCaseDeleteTrip>();
builder.Services.AddScoped<UseCaseUpdateTrip>();
builder.Services.AddScoped<UseCaseFetchTripByFilter>();
builder.Services.AddScoped<UseCaseFetchTripByFilterPassenger>();
builder.Services.AddScoped<BookingRepository>();


//Use Case Booking
builder.Services.AddScoped<UseCaseFetchAllBooking>();
builder.Services.AddScoped<UseCaseCreateBooking>();
builder.Services.AddScoped<UseCaseFetchBookingById>();
builder.Services.AddScoped<UseCaseDeleteBooking>();
builder.Services.AddScoped<UseCaseFetchBookingByFilter>();

//Use Case User
builder.Services.AddScoped<UseCaseFetchAllUser>();
builder.Services.AddScoped<UseCaseFetchUserById>();
builder.Services.AddScoped<UseCaseCreateUser>();
builder.Services.AddScoped<UseCaseUpdateUser>();
builder.Services.AddScoped<UseCaseDeleteUser>();
builder.Services.AddScoped<UseCaseFetchUserByEmail>();
builder.Services.AddScoped<UseCaseFetchUserByUsername>();

//Use Case Admin
builder.Services.AddScoped<UseCaseCreateAdmin>();
builder.Services.AddScoped<UseCaseUpdateAdmin>();

//Use Case Passenger
builder.Services.AddScoped<UseCaseCreatePassenger>();
builder.Services.AddScoped<UseCaseUpdatePassenger>();

//Use Case Driver
builder.Services.AddScoped<UseCaseCreateDriver>();
builder.Services.AddScoped<UseCaseUpdateDriver>();

//Use Case Authentication
builder.Services.AddScoped<UseCaseLogin>();
builder.Services.AddScoped<UseCaseRegistrationEmail>();
builder.Services.AddScoped<UseCaseRegistrationUsername>();

//JWT configuration
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configs["JwtSettings:SecretKey"]!))
        };
        options.Events = new JwtBearerEvents {
           OnMessageReceived = context =>
           {
               var token = context.Request.Cookies[configs["JwtSettings:CookieName"]!];
               if (string.IsNullOrEmpty(token)) return Task.CompletedTask;
                context.Token = token;
                return Task.CompletedTask;
            }
        };
    });

// Initialize Loggers
builder.Services.AddLogging(b =>
{
    b.AddConsole();
    b.AddDebug();
});

//SignalR
builder.Services.AddSignalR();

builder.Services.AddCors(options => {
    options.AddPolicy("Dev", policyBuilder =>
        policyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Dev");
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();