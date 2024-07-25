using Application.Services.TokenJWT;
using Application.UseCases.Authentication;
using Application.UseCases.Authentication.Dtos;
using Infrastructure.Ef.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;
    private readonly UseCaseLogin _useCaseLogin;

    // Constructor to initialize the dependencies of the AuthenticationController.
    public AuthenticationController(UseCaseLogin useCaseLogin, TokenService tokenService, IConfiguration configuration) {
        _useCaseLogin = useCaseLogin; // Initialize the use case for user login.
        _tokenService = tokenService; // Initialize the token service for generating and validating tokens.
        _configuration = configuration; // Initialize the configuration for accessing settings.
    }
    
    // Retrieve the connected user's ID and UserType from the JWT token in the cookie.
    private (string Id, string UserType) GetConnectedUserStatus() {
        // Get the token from the cookie
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!;
        // Extract user data from the token
        return _tokenService.GetAuthCookieData(token);
    }
    
    [HttpGet("isConnected")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public IActionResult IsConnected() {
        // Simply return 200 OK if the user is authorized
        return Ok(StatusCodes.Status200OK);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetUserIdAndRole() {
        try {
            // Retrieve the connected user status
            var status = GetConnectedUserStatus();
            // Return user ID and role
            return Ok(new { Id = status.Id, UserType = status.UserType });
        }
        catch (Exception) {
            // Return 401 Unauthorized if an exception occurs
            return Unauthorized();
        }
    }
    
    [HttpPost("signIn")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public IActionResult UserSignIn([FromBody] DtoInputUserSignIn inputUserSignIn) {
        try {
            // Execute login use case to get user data
            var dtoUser = _useCaseLogin.Execute(inputUserSignIn.Email);
            // Verify the provided password against the stored hash
            if (!PasswordHasher.VerifyPassword(inputUserSignIn.Password, dtoUser.Password)) return NotFound();
            // Generate a JWT token and set it in a cookie
            if (!GenerateToken(dtoUser.Id, dtoUser.UserType.ToString(), !inputUserSignIn.Logged)) return NotFound();
            return Ok();
        }
        catch (Exception e) {
            // Return a 500 error if an exception occurs
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpPost("signOut")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public IActionResult UserSignOut() {
        try {
            // Delete the cookie if it exists
            if (Request.Cookies.ContainsKey(_configuration["JwtSettings:CookieName"]!)) {
                Response.Cookies.Delete(_configuration["JwtSettings:CookieName"]!);
            }
            return Ok();
        }
        catch (Exception) {
            // Return a 500 error if an exception occurs
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging out the user.");
        }
    }
    
    // Generates a JWT token and stores it in a secure cookie.
    private bool GenerateToken(string login, string role, bool isSessionOnly) {
        try {
            // Create the JWT token with user information.
            var tokenValue = _tokenService.GenerateJwtToken(login, role, isSessionOnly);
            // Configure the cookie options.
            var cookieOptions = new CookieOptions {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            // Set the cookie expiration if it is not session-only.
            if (!isSessionOnly) {
                cookieOptions.Expires = DateTimeOffset.UtcNow.AddHours(int.Parse(_configuration["JwtSettings:ValidityHours"]!));
            }
            // Add the cookie to the response.
            Response.Cookies.Append("WaymateSession", tokenValue, cookieOptions);
            return true;
        }
        catch (Exception) {
            // Return false in case of an error.
            return false;
        }
    }
}