using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleCRMApp.Data;
using SampleCRMApp.Data.Entities;

namespace SampleCRMApp.Controllers;

public class AccountController(
    ApplicationDbContext db,
    IPasswordHasher<User> passwordHasher,
    ILogger<AccountController> logger) : Controller
{
    [HttpPost]
    [Route("/account/do-login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DoLogin(
        [FromForm] string email,
        [FromForm] string password,
        [FromForm] string? returnUrl,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return Redirect("/account/login?error=1");

        var user = await db.Users.FirstOrDefaultAsync(
            u => u.Email == email.Trim() && u.Active,
            cancellationToken);

        if (user is null)
        {
            logger.LogWarning("Login failed: user not found for {Email}", email);
            return Redirect("/account/login?error=1");
        }

        var verify = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (verify == PasswordVerificationResult.Failed)
        {
            logger.LogWarning("Login failed: bad password for {Email}", email);
            return Redirect("/account/login?error=1");
        }

        if (verify == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, password);
            await db.SaveChangesAsync(cancellationToken);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return Redirect("/");
    }

    [HttpGet]
    [Route("/account/logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/account/login");
    }
}
