using ApiPersonasDoc.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

//NuGets:
//Microsoft.EntityFrameworkCore
//Microsoft.EntityFrameworkCore.Tools
//Newtonsoft.Json
//Npgsql.EntityFrameworkCore.PosgreSQL
//MediatR
//FluentValidation.AspNetCore
//Automapper
//AutoMapper.Extensions.Microsoft.DependencyInjection

//appsettings.json:
/*  "ConnectionStrings": {
    "connectionString": "Server=localhost;Port=5432;Database=;User Id=;Password=;"
  }*/

//Consola del administrador de paquetes:    
//Previamente crear carpetas: Models, Data
//Scaffold-DbContext "Name=connectionString" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -ContextDir Data -Context ApplicationContext
//Actualizar base de datos (estructura)
//Scaffold-DbContext "Name=connectionString" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -ContextDir Data -Context ApplicationContext -f
