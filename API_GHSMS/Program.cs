using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OhBau.Service.CloudinaryService;
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
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SWP391GHSMContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IDashBoardService, DashBoardService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TestRepository>();
builder.Services.AddScoped<DashBoardRepository>();
builder.Services.AddScoped<AuthenRepository>();
builder.Services.AddScoped<IAuthenService, AuthenService>();

builder.Services.AddScoped<ConsultantRepository>();
builder.Services.AddScoped<IConsultantsService, ConsultantsService>();

builder.Services.AddScoped<BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<TestResultRepository>();
builder.Services.AddScoped<ITestResultService, TestResultSerivce>();
builder.Services.AddSignalR();

builder.Services.Configure<PayOSConfig>(
    builder.Configuration.GetSection(PayOSConfig.ConfigName));
builder.Services.AddHttpClient<IPayOSService, PayOSService.Services.PayOSService>();
builder.Services.Configure<PayOSConfig>(builder.Configuration.GetSection("PayOS"));


builder.Services.Configure<SendMailConfig>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var account = new CloudinaryDotNet.Account(
        configuration["Cloudinary:CloudName"],
        configuration["Cloudinary:ApiKey"],
        configuration["Cloudinary:Secret"]);
    return account;
}
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddSingleton(sp =>
{
    var account = sp.GetRequiredService<CloudinaryDotNet.Account>();
    return new CloudinaryDotNet.Cloudinary(account);
});

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors("Allow");
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<MessageHub>("hubs/message");
app.Run();








//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Repository.Models;
//using Repository.Repository;
//using Service.Implement;
//using Service.Interface;
//using System.Text;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

//var builder = WebApplication.CreateBuilder(args);


//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<SWP391GHSMContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
//    ));



//// Add services to the container.

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//        options.JsonSerializerOptions.WriteIndented = true;
//    });

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<SWP391GHSMContext>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<ITestService, TestService>();
//builder.Services.AddScoped<IDashBoardService,DashBoardService>();
//builder.Services.AddScoped<IFeedbackService, FeedbackService>();
//builder.Services.AddScoped<IConsultantService, ConsultantService>();
//builder.Services.AddScoped<UserRepository>();
//builder.Services.AddScoped<TestRepository>();
//builder.Services.AddScoped<DashBoardRepository>();
//builder.Services.AddScoped<AuthenRepository>();
//builder.Services.AddScoped<FeedbackRepository>();
//builder.Services.AddScoped<ConsultantRepository>();
//builder.Services.AddScoped<IAuthenService, AuthenService>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//    });
//});
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    var jwtSettings = builder.Configuration.GetSection("Jwt");
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
//    };
//});

//builder.Services.AddAuthorization();


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();
//app.UseCors("AllowAll");
//app.MapControllers();

//app.Run();
