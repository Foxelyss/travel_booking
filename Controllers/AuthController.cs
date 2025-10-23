using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace TravelBooking.Controllers;

public class AuthController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost("login")]
    public IResult Login(HttpContext context)
    {
        // получаем из формы email и пароль
        var form = context.Request.Form;
        // если email и/или пароль не установлены, посылаем статусный код ошибки 400
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Email и/или пароль не установлены");

        string email = form["email"];
        string password = form["password"];

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return Results.Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(jwt) });
    }

    [HttpPost("auth")]
    public async Task<IResult> Authorize(string? returnUrl, HttpContext context)
    {
        // получаем из формы email и пароль
        var form = context.Request.Form;
        // если email и/или пароль не установлены, посылаем статусный код ошибки 400
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Email и/или пароль не установлены");

        string email = form["email"];
        string password = form["password"];

        // находим пользователя 
        // Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
        // если пользователь не найден, отправляем статусный код 401
        // if (person is null) return Results.Unauthorized();

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };

        // создаем объект ClaimsIdentity
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        // установка аутентификационных куки
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Results.Redirect(returnUrl ?? "/");
    }

    [HttpPost("auth")]
    public async Task<IResult> Register(string? returnUrl, HttpContext context)
    {
        // получаем из формы email и пароль
        var form = context.Request.Form;
        // если email и/или пароль не установлены, посылаем статусный код ошибки 400
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Email и/или пароль не установлены");

        string email = form["email"];
        string password = form["password"];

        // находим пользователя 
        // Person? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
        // если пользователь не найден, отправляем статусный код 401
        // if (person is null) return Results.Unauthorized();

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };

        // создаем объект ClaimsIdentity
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        // установка аутентификационных куки
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Results.Redirect(returnUrl ?? "/");
    }
}

