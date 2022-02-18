using FleetManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DbContext"));
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("IsAdmin", policy => policy.RequireRole("Administrator"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "IsAdmin");
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
    if (app.Environment.IsDevelopment())
    {
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
            var adminUser = new AppUser { UserName = "admin", FirstName = "Marek", LastName = "Škalda" };
            var r = userManager.CreateAsync(adminUser, "ahojJaJsemAdmin").Result;
            await userManager.AddToRoleAsync(adminUser, "Administrator");
            if (r != IdentityResult.Success)
            {
                var errors = string.Join(", ", r.Errors.Select(x => x.Description));
                throw new Exception("Seeding default user failed: " + errors);
            }
        }
    }
}

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
