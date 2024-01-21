
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using SoccerPlus.Api.Configurations;
using SoccerPlus.Infra.CrossCutting.IoC;
using SoccerPlus.Infra.Data;
using SoccerPlus.Infra.Utils.Configurations;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("App:Settings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddLocalHttpClients(builder.Configuration);
builder.Services.AddLocalServices(builder.Configuration);
builder.Services.ResolveAuthentication(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "default");
    });
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
         .AddEntityFrameworkStores<Context>()
         .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
options.IssuerUri = "https://localhost:44393"
)
       .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
       .AddInMemoryClients(IdentityServerConfig.GetClients())
       .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
       .AddAspNetIdentity<IdentityUser>()
       .AddDeveloperSigningCredential();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConn"),
      sqlServerOptionsAction: sqlOptions =>
      {
          sqlOptions.EnableRetryOnFailure(
              maxRetryCount: 5,
              maxRetryDelay: TimeSpan.FromSeconds(30),
              errorNumbersToAdd: null);
      }
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.UseIdentityServer();

app.UseEndpoints(endpoints => endpoints.MapControllers());



app.Run();
