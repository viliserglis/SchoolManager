using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Auth;
using SchoolManager.Models.DTO.Auth;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthApplication authApplication, ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
    {
        logger.LogInformation("Login attempt for user: {Username}", loginDto.Username);

        try
        {
            var user = authApplication.ValidateUser(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                logger.LogWarning("Failed login attempt for user: {Username}", loginDto.Username);
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal, new AuthenticationProperties()
                {
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
                    AllowRefresh = true
                });

            logger.LogInformation("Successful login for user: {Username}", user.Username);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during login for user: {Username}", loginDto.Username);
            throw;
        }
    }
}