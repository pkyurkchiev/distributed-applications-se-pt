using MC.ApplicationServices.Implementations;
using MC.ApplicationServices.Interfaces;
using MC.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Logger.Debug("Application is starting");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    string assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "wrong name";
    builder.Services.AddDbContext<MoviesDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly(assemblyName)));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        // using System.Reflection;
        var xmlFilename = $"{assemblyName}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    string tokenTokey = builder.Configuration["Authentication:TokenKey"] ?? "Not working key";
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenTokey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

    builder.Services.AddSerilog();

    builder.Services.AddScoped<IMoviesService, MoviesService>();
    builder.Services.AddAuthorization();
    builder.Services.AddSingleton<IJWTAuthenticationsManager>(new JWTAuthenticationsManager(tokenTokey));


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Unhandled exception");
}
finally
{
    await Log.CloseAndFlushAsync(); // ensure all logs written before app exits
}
