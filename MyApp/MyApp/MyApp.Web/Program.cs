using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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


//Configure Autofac
builder.Host.UseServiceProviderFactory(new
AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => {
    containerBuilder
    .RegisterModule(new WebModule())
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

//This will run the application into docker container
builder.WebHost.UseUrls("http://*:80");

//Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
builder.Services.AddDbContext<TrainingDbContext>(options =>
    options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Identity Customization
builder.Services
    .AddIdentity<ApplicationUser, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager>()
    .AddRoleManager<RoleManager>()
    .AddSignInManager<SignInManager>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
        options.LogoutPath = new PathString("/Account/Logout");
        options.Cookie.Name = "CustomerPortal.Identity";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    })
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
builder.Services.AddSingleton<IAuthorizationHandler, TestRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ApiRequirementHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MerchantPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Vendor");
        policy.RequireRole("Distributor");
    });

    options.AddPolicy("TestViewPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("ViewTestPage", "true");
        policy.RequireRole("Admin");
    });

    //More robust handler for roles, policy and claims
    options.AddPolicy("TestRequirementPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new TestRequirement());
        policy.RequireRole("Vendor");
    });

    options.AddPolicy("AccessPermission", policy =>
    {
        policy.AuthenticationSchemes.Clear();
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new ApiRequirement());
    });
});

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


builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();

try
{
    var app = builder.Build();

    Log.Information("Build Successfull! Starting your application");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    //app.MapRazorPages();

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
