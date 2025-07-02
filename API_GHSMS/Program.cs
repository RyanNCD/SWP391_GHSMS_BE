using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Repository.Repository;
using Service.Implement;
using Service.Interface;
using System.Text;
using API_GHSMS.Hubs;
using System.Text.Json.Serialization;
using PayOSService.Config;
using PayOSService.Services;
using EmailService.Config;
using EmailService.Implement;
using EmailService.Interface;
using Repository.CloudinaryService;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Controllers with JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database context
builder.Services.AddDbContext<SWP391GHSMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TestRepository>();
builder.Services.AddScoped<DashBoardRepository>();
builder.Services.AddScoped<AuthenRepository>();
builder.Services.AddScoped<ConsultantsRepository>();
builder.Services.AddScoped<BlogRepository>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<TestResultRepository>();

// Services
builder.Services.AddScoped<SWP391GHSMContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IDashBoardService, DashBoardService>();
builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddScoped<IConsultantsService, ConsultantsService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITestResultService, TestResultSerivce>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

// Email config
builder.Services.Configure<SendMailConfig>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

// PayOS config
builder.Services.Configure<PayOSConfig>(builder.Configuration.GetSection("PayOS"));
builder.Services.AddHttpClient<IPayOSService, PayOSService.Services.PayOSService>();

// Cloudinary
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new CloudinaryDotNet.Account(
        config["Cloudinary:CloudName"],
        config["Cloudinary:ApiKey"],
        config["Cloudinary:Secret"]
    );
});

builder.Services.AddSingleton(sp =>
{
    var account = sp.GetRequiredService<CloudinaryDotNet.Account>();
    return new CloudinaryDotNet.Cloudinary(account);
});

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Middleware pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Allow");
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<MessageHub>("hubs/message");

app.Run();
