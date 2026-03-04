using Application;
using Application.Config;
using Application.Interfaces;
using Application.Middlewares;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;


IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("AppSettings.json", false, true);

IConfigurationRoot configurationRoot = configurationBuilder.Build();

var settingSection = configurationRoot.GetRequiredSection(nameof(Settings));
Settings? settings = settingSection.Get<Settings>();

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

/*builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.JWTSettings["Audience"],
            ValidAudience = settings.JWTSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JWTSettings["Secret"])),
            RoleClaimType = ClaimTypes.Role // Important!
        };
    });*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

builder.Services.AddAuthenticationJwtBearer(options => options.SigningKey = settings.JWTSettings["Secret"]).AddAuthorization();

builder.Services.Configure<Settings>(settingSection);

builder.Services.AddFastEndpoints().SwaggerDocument();

builder.Services.Configure<FormOptions>(opt =>
{
    opt.MultipartBodyLengthLimit = 1024 * 1024 * 50;
    opt.MultipartHeadersLengthLimit = 1024 *1024 * 50;
});

builder.WebHost.ConfigureKestrel(opt =>
{
    opt.Limits.MaxRequestBodySize = 1024 * 1024 * 100;
});

builder.Services.AddInfrastructure(settings);

builder.Services.AddApplication(settings);

var app = builder.Build();

IMigration migration = app.Services.GetService<IMigration>();
migration.CreateTables();
migration.InsertRoles();

app.UseCors("corsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();
