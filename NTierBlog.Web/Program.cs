using Microsoft.AspNetCore.Identity;
using NTierBlog.Data.Context;
using NTierBlog.Data.Extensions;
using NTierBlog.Entity.Entities;
using NTierBlog.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
	opt.Password.RequireLowercase =false;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireUppercase = false;
})
	.AddRoleManager<RoleManager<AppRole>>()
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
	config.LoginPath = new PathString("/Admin/Auth/Login"); //Admin area, Auth controller, Login action
	config.LogoutPath = new PathString("/Admin/Auth/Logout"); //Admin area, Auth controller, Logout action
	config.Cookie = new CookieBuilder
	{
		Name = "NTierBlog",
		HttpOnly = true,
		SameSite = SameSiteMode.Strict,
		SecurePolicy = CookieSecurePolicy.SameAsRequest //Always
	};
	config.SlidingExpiration = true;
	config.ExpireTimeSpan = TimeSpan.FromDays(7);
	config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //Admin area, Auth controller, AccessDenied action
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
app.UseSession();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{
	endpoints.MapAreaControllerRoute(
		name: "Admin",
		areaName: "Admin",
		pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
	);
	endpoints.MapDefaultControllerRoute().WithStaticAssets();
});


app.Run();
