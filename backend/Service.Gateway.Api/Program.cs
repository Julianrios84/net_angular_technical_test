using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers();
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile($"ocelot.json")
                            .Build();
builder.Services.AddOcelot(configuration);

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


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseOcelot().Wait();

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

