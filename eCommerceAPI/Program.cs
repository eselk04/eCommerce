using eCommerce.Application;
using eCommerce.Application.Exceptions;
using eCommerce.Infrastructure;
using eCommerce.Infrastructure.Enums;
using eCommerce.Persistence;
using Mapper;using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddStorage(StorageType.storagetype.Local);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCustomMapper();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}




app.UseHttpsRedirection();

app.UseAuthorization();
app.ConfigureExceptionHandlingMiddleware();
app.MapControllers();

app.Run();
