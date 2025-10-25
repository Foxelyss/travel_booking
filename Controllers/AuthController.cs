using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using TravelBooking.Data;
using TravelBooking.DTO;
using TravelBooking.Models;

namespace TravelBooking.Controllers;

public class AuthController : Controller
{
    private readonly StoreContext _context;

    public AuthController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }



    [HttpPost("api/auth/login")]
    public IResult Login()
    {
        HttpContext context = HttpContext;
        // получаем из формы email и пароль
        var form = context.Request.Form;
        // если email и/или пароль не установлены, посылаем статусный код ошибки 400
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Email и/или пароль не установлены");

        string email = form["email"];
        string password = form["password"];

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, email), new Claim(ClaimTypes.Role, "regular") };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(4)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return Results.Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(jwt) });
    }

    [HttpPost("login")]
    public async Task Authorize(string? returnUrl)
    {
        // получаем из формы email и пароль
        var form = HttpContext.Request.Form;
        // если email и/или пароль не установлены, посылаем статусный код ошибки 400
        if (!form.ContainsKey("username") || !form.ContainsKey("password"))
            // return Results.BadRequest("Email и/или пароль не установлены");
            ;

        string email = form["username"];
        string password = form["password"];

        // находим пользователя 
        // Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
        // если пользователь не найден, отправляем статусный код 401
        // if (person is null) return Results.Unauthorized();

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, email), new Claim(ClaimTypes.Role, "regular") };

        // создаем объект ClaimsIdentity
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        // установка аутентификационных куки
        var prop = new AuthenticationProperties()
        {
            RedirectUri = "/Index"
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), prop);
        // return Results.Redirect(returnUrl ?? "/");
        // await HttpContext.SignInAsync("oidc", );
    }

    [HttpPost("register")]
    [Consumes("application/json")]
    public IResult Register([FromBody] AccountRegistration accountRegistration)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var account = new Account
        {
            Email = accountRegistration.email,
            PasswordHash = accountRegistration.password,
            Phone = accountRegistration.phone,
            Username = accountRegistration.username ?? accountRegistration.email
        };
        _context.Accounts.Add(account);

        _context.SaveChanges();
        return Results.Ok(account);
    }

    [HttpGet("api/auth/about")]
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    public IResult AboutUser()
    {
        return Results.Ok(HttpContext.User.HasClaim(ClaimTypes.Role, "regular"));
    }

    [HttpGet("logout")]
    [Authorize]
    public async Task Logout(string? returnUrl)
    {
        Console.WriteLine("asdfa!@!");
        var prop = new AuthenticationProperties()
        {
            RedirectUri = "index.html"
        };
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync("oidc", prop);
    }
}

