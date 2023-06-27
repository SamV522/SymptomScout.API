using Microsoft.EntityFrameworkCore;
using SymptomScout.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("SymptomScoutDb");
}
else
{
    connection = Environment.GetEnvironmentVariable("SymptomScoutDb");
}

builder.Services.AddDbContext<SymptomScoutDbContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddCors(
    options => options.AddPolicy("Default", builder =>
        builder.WithOrigins("http://localhost:4200", "https://symptomscout-app.azurewebsites.net/", "https://symptomscout.com/")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("Default");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
