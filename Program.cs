using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Configuration;
using MomsKitchen.Database;
using MomsKitchen.Database.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddDbContext<MomsKitchenContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<MomsKitchenContext>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddFastEndpoints().SwaggerDocument();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.ConfigureOptions<AuthenticationOptionsSetup>();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

app.UseCors(policyBuilder => policyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseFastEndpoints()
    .UseSwaggerGen();

app.Run();