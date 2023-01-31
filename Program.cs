using System.Text;
using AutoWrapper;
using gisAPI.Auth;
using gisAPI.Interfaces;
using gisAPI.Persistence;
using gisAPI.Security;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(@"Logs\logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("Starting up");
try
{ }
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console()
       .WriteTo.File(@"Logs\logs.txt", rollingInterval: RollingInterval.Day)
       .ReadFrom.Configuration(ctx.Configuration));
// Add services to the container.
builder.Services.AddTransient<IDbContext>(_ => new DbContext(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GIS API", Version = "v1" });
        c.CustomSchemaIds(x => x.FullName);
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert Token with Bearer into field",
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(opt =>
      {
          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]));

          opt.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = key,
              ValidateAudience = false,
              ValidateIssuer = false,
              ValidateLifetime = true,
              ClockSkew = TimeSpan.Zero
          };
      });

builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddMediatR(typeof(RegisterUser).Assembly);
builder.Services.AddAutoMapper(typeof(RegisterUser).Assembly);
builder.Services.AddMediatR(typeof(LoginUser).Assembly);
builder.Services.AddAutoMapper(typeof(LoginUser).Assembly);
builder.Services.AddSingleton(typeof(ISqlGenerator<>), typeof(SqlGenerator<>));
builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
app.UseStatusCodePages();
// }
if (app.Environment.IsDevelopment())
    app.UseCors(c => c
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
     .WithOrigins("http://localhost:4200", "http://localhost:3131"));
else
    app.UseCors(c => c
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseDefaultFiles();


app.MapControllers();

app.MapFallbackToController("Index", "Fallback");

app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
{
    IsDebug = app.Environment.IsDevelopment(),
    IsApiOnly = false,
    IgnoreWrapForOkRequests = true,
});

app.UseSerilogRequestLogging();

app.Run();
