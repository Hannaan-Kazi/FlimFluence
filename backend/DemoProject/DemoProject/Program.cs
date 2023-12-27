using DataService;
using DataService.Abstract;
using DataService.Services;
using DemoProject.Controllers;
using Managers.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddDbContext<ProjectDbContext>(
    options => options.UseSqlServer(
        "Data Source=LP013\\SQLEXPRESS;Initial Catalog=ProjectDb;User ID=api;Password=root123;TrustServerCertificate=True"));

builder.Services.AddTransient<IUserDataService, UserDataService>();
builder.Services.AddTransient<UsersManager>();
builder.Services.AddTransient<User>();

builder.Services.AddTransient<IAdminDataService, AdminDataService>();
builder.Services.AddTransient<AdminsManager>();
builder.Services.AddTransient<Admin>();

builder.Services.AddTransient<IMovieDataService, MovieDataService>();
builder.Services.AddTransient<MoviesManager>();
//builder.Services.AddTransient<Movie>();

builder.Services.AddTransient<IWatchLaterDataService, WLDataService>();
builder.Services.AddTransient<WLManager>();

builder.Services.AddTransient<IRatingDataService, RatingDataService>();
builder.Services.AddTransient<RatingsManager>();

builder.Services.AddTransient<IWatchedDataservice, WatchedDataService>();
builder.Services.AddTransient<WatchedsManager>();

builder.Services.AddTransient<ITestDataService, TestDataService>();
builder.Services.AddTransient<TestManager>();


//builder.Services.AddCors(options => options.AddPolicy(name: "Movies",
//    policy =>{
//        policy.WithOrigins("").AllowAnyMethod().AllowAnyHeader(); }));





//builder.Services.Configure<JwtSettingscs>(builder.Configuration.GetSection("Jwt"));

var authkey = builder.Configuration.GetValue<String>("Jwt:Key");

Console.WriteLine("Before JwtBearer authentication configuration.");

builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
