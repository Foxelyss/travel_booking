using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
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
    private readonly PasswordHasher<string> _hasher = new PasswordHasher<string>();

    public AuthController(StoreContext context)
    {
        _context = context;
    }

    [HttpPost("api/auth/login")]
    [Consumes("application/x-www-form-urlencoded")]
    public IResult Login()
    {
        HttpContext context = HttpContext;

        var form = context.Request.Form;
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Email и/или пароль не установлены");

        string email = form["email"];
        string password = form["password"];

        var account = _context.Accounts.Where(p => p.Email == email).FirstOrDefault();

        if (account == null)
        {
            return Results.Unauthorized();
        }

        var authStatus = _hasher.VerifyHashedPassword("", account.PasswordHash, password);
        switch (authStatus)
        {
            case PasswordVerificationResult.Failed:
                return Results.Unauthorized();
            case PasswordVerificationResult.SuccessRehashNeeded:
                break;
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()), new Claim(ClaimTypes.Role, "regular") };

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(30)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return Results.Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(jwt) });
    }

    [HttpPost("api/auth/register")]
    [Consumes("application/x-www-form-urlencoded")]
    public IResult RegisterFromAPI([FromForm] AccountRegistration accountRegistration)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        if (accountRegistration.password.IndexOf(' ') != -1)
        {
            return Results.BadRequest("Пароль имеет пробелы");
        }

        var account = new Account
        {
            Email = accountRegistration.email,
            PasswordHash = _hasher.HashPassword("", accountRegistration.password),
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
        return Results.Ok(_context.Accounts.Find(HttpContext.User.GetGuid()));
    }

    [HttpGet("api/auth/logout")]
    [Authorize]
    public async Task<IResult> LogoutAsd(string? returnUrl)
    {
        var prop = new AuthenticationProperties()
        {
            RedirectUri = "index.html"
        };
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Results.Redirect(returnUrl ?? "/");
    }
}

