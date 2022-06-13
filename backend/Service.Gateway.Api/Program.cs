using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Middleware;

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

/* builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
}); */



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(p => p.AddPolicy("CorsRule", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));




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

