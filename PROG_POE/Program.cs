using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROG_POE.Areas.Identity.Data;
using PROG_POE.Data;
using PROG_POE.Models.Domain;
using System.Security.Claims;

//Refrences

//- Identity 
//The following method was taken from youtube
// Author: Digtal TechJoint
//Link: https://youtu.be/ghzvSROMo_M?si=Y1YzZVwhciU1dcUm
// For MVC and asp.net
// Troelsen,A and Japikse, P.2021. Foundational Priciples and Practices in Programming. 10th ed.New York: Welmoed Spahr

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ModAppIdentityDbC>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ModAppIdentityDbCConnection")));

builder.Services.AddDbContext<ModuleAppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ModAppIdentityDbCConnection")));


builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ModAppIdentityDbC>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Other configuration settings...
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.Password.RequireNonAlphanumeric = false;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;
app.MapRazorPages();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=LogLandingPage}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});



app.Run();
