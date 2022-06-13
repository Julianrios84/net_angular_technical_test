using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Service.Security.Api.Core.Application;
using Service.Security.Api.Core.Entities;
using Service.Security.Api.Core.Persistence;
using Service.Security.Api.Core.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Fluent Validator -> library
builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Register>());
// connection database -> SQL Server
builder.Services.AddDbContext<SecurityContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

// Configuration IdentityCore  
var identityCore = builder.Services.AddIdentityCore<User>();
var indentityBuilder = new IdentityBuilder(identityCore.UserType, identityCore.Services);

indentityBuilder.AddEntityFrameworkStores<SecurityContext>();
indentityBuilder.AddSignInManager<SignInManager<User>>();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
// MediatR
builder.Services.AddMediatR(typeof(Register.UserRegisterCommand).Assembly);
// AutoMapper
builder.Services.AddAutoMapper(typeof(Register.UserRegisterHandler));

// Security Json Web Token
builder.Services.AddScoped<IJsonWebTokenGenerator, JsonWebTokenGenerator>();
builder.Services.AddScoped<IUserInSession, UserInSession>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C7q3FBCJZq0bIRRH0Dq4lxWuBipEBkHX"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

/* Cors -> Create rules
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
}); */

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("CorsRule", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

using (var context = app.Services.CreateScope())
{
    var services = context.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var contextef = services.GetRequiredService<SecurityContext>();

        SecurityData.InsertUser(contextef, userManager).Wait();
    }
    catch (Exception e)
    {

        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "Error when registering user");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors Rule enabled
app.UseCors("CorsRule");

// Authentificaction enabled
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
