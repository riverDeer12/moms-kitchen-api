using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MomsKitchen.Api.Database;
using MomsKitchen.Api.Database.Entities;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddFastEndpoints();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MomsKitchenContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MomsKitchenContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseFastEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();