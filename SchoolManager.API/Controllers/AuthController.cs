using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Auth;
using SchoolManager.Models.DTO.Auth;
using SchoolManager.Repository.Repositories.UserRepository;

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

    [HttpPost]
    [Route("create-user")]
    public int CreateUser([FromBody] CreateUserDTO createUserDto)
    {
        logger.LogInformation("Creating new user: {Username} with role: {Role}", createUserDto.Username, createUserDto.Role);

        try
        {
            var userId = authApplication.CreateUser(createUserDto.Username, createUserDto.Password, createUserDto.Role);
            logger.LogInformation("Successfully created user with ID: {UserId}", userId);
            return userId;
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Username {Username} already exists", createUserDto.Username);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating user: {Username}", createUserDto.Username);
            throw;
        }
    }

    [HttpPost]
    [Route("set-password")]
    public ActionResult<bool> SetPassword([FromBody] SetPasswordDTO setPasswordDto)
    {
        logger.LogInformation("Setting password for user ID: {UserId}", setPasswordDto.UserId);

        try
        {
            var result = authApplication.SetPassword(setPasswordDto.UserId, setPasswordDto.NewPassword);
            if (result)
            {
                logger.LogInformation("Successfully set password for user ID: {UserId}", setPasswordDto.UserId);
                return Ok(true);
            }
            else
            {
                logger.LogWarning("User not found with ID: {UserId}", setPasswordDto.UserId);
                return NotFound("User not found");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error setting password for user ID: {UserId}", setPasswordDto.UserId);
            throw;
        }
    }

    [HttpPost]
    [Route("update-role")]
    public ActionResult<bool> UpdateUserRole([FromBody] UpdateUserRoleDTO updateUserRoleDto)
    {
        logger.LogInformation("Updating role for user ID: {UserId} to {NewRole}", updateUserRoleDto.UserId, updateUserRoleDto.NewRole);

        try
        {
            var result = authApplication.UpdateUserRole(updateUserRoleDto.UserId, updateUserRoleDto.NewRole);
            if (result)
            {
                logger.LogInformation("Successfully updated role for user ID: {UserId} to {NewRole}", updateUserRoleDto.UserId, updateUserRoleDto.NewRole);
                return Ok(true);
            }
            else
            {
                logger.LogWarning("User not found with ID: {UserId}", updateUserRoleDto.UserId);
                return NotFound("User not found");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating role for user ID: {UserId}", updateUserRoleDto.UserId);
            throw;
        }
    }
}