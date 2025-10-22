using TravelBooking;
using TravelBooking.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("StoreContext");
builder.Services.AddSqlite<StoreContext>(connectionString);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapControllerRoute(
    name: "administration",
    pattern: "{controller=Administration}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "book",
pattern: "{controller=Book}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "search",
pattern: "{controller=Search}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "transport",
pattern: "{controller=Transport}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "company",
pattern: "{controller=Company}/{action=Index}/{id?}");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MigrateDB();

app.Run();
