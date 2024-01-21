using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SoccerPlus.Application.Player.Services;
using SoccerPlus.Application.User.Services;
using SoccerPlus.Domain.PlayerAggregate;
using SoccerPlus.Domain.SoccerMatchAggregate;
using SoccerPlus.Infra.Data.Repositories;
using SoccerPlus.Infra.Http.Authentication;
using SoccerPlus.Infra.Http.Email;
using SoccerPlus.Infra.Http.Mapbox;
using SoccerPlus.Infra.Utils.Configurations;
using System.Text;
using static IdentityModel.OidcConstants.Algorithms;

namespace SoccerPlus.Infra.CrossCutting.IoC;

public static class NativeInjector
{
    public static void ResolveAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration.GetSection("App:Settings").Get<TokenSettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = appSettings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = appSettings.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    };
                });

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = "apiKey",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "Insert JWT Token this way: Bearer {seu token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });
        });

    }

    public static void AddLocalHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var urlMapbox = configuration["App:Settings:MapboxUrl"];
        var urlAuthentication = configuration["App:Settings:IS4"];

        services.AddHttpClient<IMapboxService, MapboxService>(c =>
        {
            c.BaseAddress = new Uri(urlMapbox);
            c.Timeout = TimeSpan.FromMinutes(10);
        });

        services.AddHttpClient<IAuthenticateService, AuthenticateService>(c =>
        {
            c.BaseAddress = new Uri(urlAuthentication);
            c.Timeout = TimeSpan.FromMinutes(10);
        });

    }
    public static void AddLocalServices(this IServiceCollection services, IConfiguration configuration)
    {

        #region Repositories
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ISoccerMatchRepository, SoccerMatchRepository>();

        #endregion
        services.AddScoped<IPlayerServices, PlayerServices>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserService, UserService>();

        #region Services


        #endregion
    }

}