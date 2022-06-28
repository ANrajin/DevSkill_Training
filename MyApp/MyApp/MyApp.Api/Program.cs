using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApp.Api;
using MyApp.Membership;
using MyApp.Membership.DbContexts;
using MyApp.Membership.Entities;
using MyApp.Membership.Securities;
using MyApp.Membership.Services;
using MyApp.Training;
using MyApp.Training.DbContexts;
using MyApp.Web;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var assemblyName = Assembly.GetExecutingAssembly().FullName;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

//Configure Autofac
builder.Host.UseServiceProviderFactory(new
AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => {
    containerBuilder
    .RegisterModule(new ApiModule())
    .RegisterModule(new MembershipModule(connectionString, assemblyName))
    .RegisterModule(new TrainingModule(connectionString, assemblyName));
});

//Configure Serilog. [File, MSSql Server]
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
);

//Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//DbContexts
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
builder.Services.AddDbContext<TrainingDbContext>(options =>
    options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));

//Identity Customization
builder.Services
    .AddIdentity<ApplicationUser, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager>()
    .AddRoleManager<RoleManager>()
    .AddSignInManager<SignInManager>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

//JWT Authentication
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

//Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AccessPermission", policy =>
    {
        policy.AuthenticationSchemes.Clear();
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new ApiRequirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, ApiRequirementHandler>();
builder.Services.AddHttpContextAccessor();

/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSites",
        builder =>
        {
            builder.WithOrigins("https://localhost:44332", "http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
        });
});
*/

//App Default
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

try
{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal("Oop! Something went wrong while building the application");
}
finally
{
    Log.CloseAndFlush();
}
