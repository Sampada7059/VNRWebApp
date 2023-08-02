using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VNRWEBAPI;
using Microsoft.EntityFrameworkCore;
using VNRWEBAPI.Models;
using VNRWEBAPI.Repository;

var builder = WebApplication.CreateBuilder(args);
//Scaffold-DbContext -Connection Name=CollegeDB Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CollegeDatabaseContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("CollegeDB")));
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ILatestUpdateRepository, LatestUpdateRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var app = builder.Build();

// Enable CORS
app.UseCors(policy =>
{
    policy.WithOrigins("https://localhost:44308")
          .AllowAnyHeader()
          .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
