using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NSwag.AspNetCore;
using TravelBooking;
using TravelBooking.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("StoreContext");
builder.Services.AddNpgsql<StoreContext>(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Агрегатор";
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");

builder.Services.AddAuthorization();
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapControllerRoute(
name: "auth",
pattern: "{controller=Auth}/{action=Index}/{id?}");

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

app.MapControllerRoute(
name: "mean",
pattern: "{controller=Mean}/{action=Index}/{id?}");

app.MapControllerRoute(
name: "point",
pattern: "{controller=Point}/{action=Index}/{id?}");

app.MapGet("api/health", () => Results.Ok());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseOpenApi();
    app.UseSwaggerUi((x) => { x.DocumentTitle = "Агрегатор"; });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MigrateDB();

app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysup2rsecret_secrets5cretsecretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}