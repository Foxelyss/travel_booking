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

    public string HashPassword(string password)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        Console.WriteLine($"Hashed: {hashed}");
        return hashed;
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
    public async Task<IResult> Authorize(string? returnUrl)
    {
        // получаем из формы email и пароль
        var form = HttpContext.Request.Form;
        // если email и/или пароль не установлены, посылаем статусный код ошибки 400
        if (!form.ContainsKey("username") || !form.ContainsKey("password"))
            return Results.BadRequest("Email и/или пароль не установлены");

        string email = form["username"];
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

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, email), new Claim(ClaimTypes.Role, "regular") };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Results.Redirect(returnUrl ?? "/");
    }

    [HttpPost("register")]
    [Consumes("application/x-www-form-urlencoded")]
    public IResult Register([FromForm] AccountRegistration accountRegistration)
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

    [HttpPost("api/auth/register")]
    [Consumes("application/json")]
    public IResult RegisterFromAPI([FromBody] AccountRegistration accountRegistration)
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
        return Results.Ok(HttpContext.User.HasClaim(ClaimTypes.Role, "regular"));
    }

    [HttpGet("logout")]
    [Authorize]
    public async Task<IResult> Logout(string? returnUrl)
    {
        Console.WriteLine("asdfa!@!");
        var prop = new AuthenticationProperties()
        {
            RedirectUri = "index.html"
        };
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Results.Redirect(returnUrl ?? "/");
    }

    [HttpGet("api/auth/logout")]
    [Authorize]
    public async Task<IResult> LogoutAsd(string? returnUrl)
    {
        Console.WriteLine("asdfa!@!");
        var prop = new AuthenticationProperties()
        {
            RedirectUri = "index.html"
        };
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Results.Redirect(returnUrl ?? "/");
    }
}

