using Microsoft.EntityFrameworkCore;
using PieShop.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PieShopDbContextConnection") ?? throw new InvalidOperationException("Connection string 'PieShopDbContextConnection' not found.");
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); // Ignore cylic dependencies, needed in case of API response with cycles. For eg. Pies referes categories and then categoreis again has reference of pies


builder.Services.AddRazorPages();
builder.Services.AddDbContext<PieShopDbContext>(options =>
{ 
	options.UseSqlServer(builder.Configuration["ConnectionStrings:PieShopDbContextConnection"]);
});

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<PieShopDbContext>();
builder.Services.AddDefaultIdentity<IdentityUser>()
	.AddEntityFrameworkStores<PieShopDbContext>();



var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute(); // "{controller=Home}/{action=Index}/{id?}"
//app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

DbInitializer.Seed(app);

app.Run();
