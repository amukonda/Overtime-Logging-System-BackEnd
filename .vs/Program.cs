using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;
using CBZ_OvertTime_Logging.Mappers;
using System;
using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DatabaseContext;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Services;
using CBZ_OverTime_Logging.Interfaces;
using CBZ_OverTime_Logging.Services;

var builder = WebApplication.CreateBuilder(args);

// Email settings configuration
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Register services


// Database context setup
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnect")));

// Service registrations
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<IOvertimeClaimService, OvertimeClaimService>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<ISubsidiaryService, SubsidiaryService>();
builder.Services.AddScoped<IUnitsService, UnitsService>();


// Add AutoMapper with the mapping profile
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Registering the MappingProfile

// Add controllers
builder.Services.AddControllers();

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Email Service API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

