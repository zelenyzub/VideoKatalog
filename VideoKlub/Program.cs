using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoKlub.Data;
using VideoKlub.Repositories.Interfaces;
using VideoKlub.Repositories.Implementation;
using VideoKlub.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true) //config email confirmation true/false
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//video repository
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
//category repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//rate repository
builder.Services.AddScoped<IRateRepository, RateRepository>();
//favorite repository
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
//user repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
//report repository
builder.Services.AddScoped<IReportRepository, ReportRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


//SEEDING ADMIN and USER ROLES
using(var scope = app.Services.CreateScope())
{
    //ADMIN & USER ROLES SEEED
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };

    foreach(var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

//SEEDING USERS WITH ADMIN & USER ROLE
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // ----- ADMIN -----
    string adminEmail = "admin@gmail.com";
    string adminPassword = "Admin123!";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // ----- REGULAR USER -----
    string userEmail = "user@gmail.com";
    string userPassword = "User123!";

    if (await userManager.FindByEmailAsync(userEmail) == null)
    {
        var regularUser = new IdentityUser
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(regularUser, userPassword);
        await userManager.AddToRoleAsync(regularUser, "User");
    }
}


app.Run();
