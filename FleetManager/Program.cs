using FleetManager.Data;
using FleetManager.Services;
using FleetManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DbContext"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("IsManager", policy => policy.RequireRole("Manager", "Administrator"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "IsAdmin");
    options.Conventions.AuthorizeFolder("/Manager", "IsManager");
}).AddRazorRuntimeCompilation();

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 12;
    options.Password.RequiredUniqueChars = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromSeconds(0);
});

builder.Services.AddTransient<ICarService, CarService>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<ICarReservationService, CarReservationService>();

builder.Services.AddTransient<INoteService, NoteService>();

builder.Services.AddTransient<ILogService, LogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.Urls.Add("http://*:80");
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    if (!roleManager.Roles.Any())
    {
        await roleManager.CreateAsync(new AppRole { Name = "User" });
        await roleManager.CreateAsync(new AppRole { Name = "Manager" });
        await roleManager.CreateAsync(new AppRole { Name = "Administrator" });
    }
    if (!userManager.Users.Any())
    {
        var adminUser = new AppUser { UserName = "admin", FirstName = "Marek", LastName = "�kalda" };
        var r = userManager.CreateAsync(adminUser, "ahojJaJsemAdmin").Result;
        await userManager.AddToRoleAsync(adminUser, "Administrator");
        if (r != IdentityResult.Success)
        {
            var errors = string.Join(", ", r.Errors.Select(x => x.Description));
            throw new Exception("Seeding default user failed: " + errors);
        }
    }
}

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
