using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using REACT_API.Models;
using REACT_API.Utils;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Hangfire;
using Hangfire.MySql;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.UTF8.GetBytes("YourVeryLongSecureSecretKeyHere1234567890!@#$");
// Add services to the container.
builder.Services.AddDbContext<MAIN01DbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))); // Adjust MySQL version if necessary

builder.Services.AddTransient<EmailHelper>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = "YourAppIssuer",

        ValidAudience = "YourAppAudience",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddHangfire(config =>
{
    config.UseStorage(new MySqlStorage(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlStorageOptions
        {
            TablesPrefix = "Hangfire_",      // Optional: Adds prefix to tables for organization
            TransactionIsolationLevel = (System.Transactions.IsolationLevel?)System.Data.IsolationLevel.ReadCommitted, // Optional
            QueuePollInterval = TimeSpan.FromSeconds(15), // Optional
            JobExpirationCheckInterval = TimeSpan.FromHours(1) // Optional
        }));
});

builder.Services.AddHangfireServer();
// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowCredentials()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication();
// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHangfireDashboard("/hangfire");
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllers();

app.Run();