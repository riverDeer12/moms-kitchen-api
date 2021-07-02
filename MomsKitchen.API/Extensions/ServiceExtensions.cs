using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MomsKitchen.API.Repositories;
using MomsKitchen.API.Services;
using MomsKitchen.API.Services.ApplicationUsers;
using MomsKitchen.API.Services.Auth;
using MomsKitchen.API.Services.Categories;
using MomsKitchen.API.Services.Recipes;
using MomsKitchen.DATA;
using MomsKitchen.DATA.Entities;

namespace MomsKitchen.API.Extensions
{
 public static class ServiceExtensions
    {
        public static void AddControllerServices(
            this IServiceCollection services
        )
        {
            services
                .AddScoped(typeof(IControllerService<,,>),
                typeof(ControllerService<,,>));
            services.AddScoped<IRecipesService, RecipesService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Model>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                {

                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Moms Kitchen API",
                        Version = "v1"
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
                });
        }

        public static void ConfigureAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var secretKey = Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWTSecretKey"]);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;
                    x.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(secretKey),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero
                        };
                });
        }

        public static void AddDbContextConfig(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddDbContext<Model>(options =>
                {
                    options
                        .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
        }
    }
}