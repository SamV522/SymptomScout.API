using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SymptomScout.API.Persistence;
using System.Net.Http.Headers;

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
        builder.WithOrigins("http://localhost:4200", "https://symptomscout-app.azurewebsites.net", "https://www.symptomscout.com")
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
}else
{
}

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        context.Response.Headers.Add(HeaderNames.AccessControlAllowOrigin, "*");

        await context.Response.WriteAsync("An exception was thrown.");

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature != null)
            await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.ToString());
    });
});

app.UseCors("Default");

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
