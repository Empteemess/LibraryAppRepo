using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using DAL.Data;
using DAL.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DAL.Configurations.DepedencyConf;

public static class DepedencyInjection
{
    public static IServiceCollection AddDepedencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<LibraryDbContext>(sq => sq.UseSqlServer(config.GetConnectionString("ConString")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuthorBookRepository, AuthorBookRepository>();
        services.AddScoped<IBookUserRepository, BookUserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SigninKey"])),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidateIssuer = true,
                    ValidAudience = config["JWT:Audience"],
                    ValidateAudience = true,
                    RoleClaimType = ClaimTypes.Role
                };
            });
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        services.AddAuthorization();

        return services;
    }
}