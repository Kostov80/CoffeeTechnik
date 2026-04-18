using CoffeeTechnik.Data;
using CoffeeTechnik.Services;
using CoffeeTechnik.Services.Implementations;
using CoffeeTechnik.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddScoped<ICoffeeService, CoffeeService>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();

var app = builder.Build();

//app.UseExceptionHandler("/Home/Error500");
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{

    app.UseExceptionHandler("/Home/Error500");
    app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}" );




using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}



using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "Technician", "Sales" };

    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).Result)
        {
            roleManager.CreateAsync(new IdentityRole(role)).Wait();
        }
    }

    
    string adminEmail = "admin@admin.com";
    string adminPassword = "Admin123!";

    var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

    if (adminUser == null)
    {
        var user = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail
        };

        var result = userManager.CreateAsync(user, adminPassword).Result;

        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}

app.Run();