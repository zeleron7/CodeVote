using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Identity;
using CodeVote.Data;
using CodeVote.src.DbModels;
using CodeVote.src.Services;
using CodeVote.src.Services.Interfaces;
using CodeVote.src.Utils;
using static CodeVote.src.Utils.RemoveStringDefaultsSchemaFilter;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CodeVoteContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CodeVoteContext")
            ?? throw new InvalidOperationException("Connection string 'CodeVoteContext' not found.")));

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// JWT Authentication
#region JWT Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
#endregion JWT Auth

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // RemoveStringDefaultsSchemaFilter to remove default values for string properties
    options.SchemaFilter<ClearStringExamplesSchemaFilter>();

    // JWT Authentication in Swagger
    #region JWT Auth
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
          },
          new string[] {}
        }
      });
    #endregion 
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI for services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectIdeaService, ProjectIdeaService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<ISeedDatabaseService, SeedDatabaseService>();

// IpasswordHasher for UserDBM (used in UserService)
builder.Services.AddScoped<IPasswordHasher<UserDbM>, PasswordHasher<UserDbM>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Security 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
