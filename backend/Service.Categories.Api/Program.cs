using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Categories.Api.Core.Application;
using Service.Categories.Api.Core.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<Create>());

builder.Services.AddDbContext<CategoryContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Database")), ServiceLifetime.Transient);

// MediatR
builder.Services.AddMediatR(typeof(Create.Manager).Assembly);
// AutoMapper
builder.Services.AddAutoMapper(typeof(Create.Run));

// Cors -> Create rules
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors Rule enabled
app.UseCors("CorsRule");

app.UseAuthorization();

app.MapControllers();

app.Run();
