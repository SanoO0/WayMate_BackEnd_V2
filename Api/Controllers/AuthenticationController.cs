using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Application.Services.TokenJWT;
using Application.Services.TokenJWT.dto;
using Application.UseCases.Authentication;
using Application.UseCases.Authentication.Dtos;
using Application.UseCases.Users.Passenger;
using Application.UseCases.Users.Passenger.Dto;
using Application.UseCases.Users.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;
    private readonly UseCaseCreatePassenger _useCaseCreatePassenger;
    private readonly UseCaseLogin _useCaseLogin;
    private readonly UseCaseRegistrationEmail _useCaseRegistrationEmail;
    private readonly UseCaseRegistrationUsername _useCaseRegistrationUsername;
    private readonly UseCaseFetchUserByEmail _userCaseFetchUserByEmail;

    public AuthenticationController(UseCaseLogin useCaseLogin, UseCaseRegistrationEmail useCaseRegistrationEmail,
        UseCaseRegistrationUsername useCaseRegistrationUsername, TokenService tokenService,
        IConfiguration configuration, UseCaseFetchUserByEmail userCaseFetchUserByEmail,
        UseCaseCreatePassenger useCaseCreatePassenger)
    {
        _useCaseLogin = useCaseLogin;
        _useCaseRegistrationEmail = useCaseRegistrationEmail;
        _useCaseRegistrationUsername = useCaseRegistrationUsername;
        _tokenService = tokenService;
        _configuration = configuration;
        _userCaseFetchUserByEmail = userCaseFetchUserByEmail;
        _useCaseCreatePassenger = useCaseCreatePassenger;
    }

    [AllowAnonymous]
    [HttpGet("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DtoOutputToken> Login([FromQuery] [Required] string email,
        [FromQuery] [Required] string password)
    {
        try
        {
            var authResult = _useCaseLogin.Execute(email, password);
            if (!authResult.isLogged) return BadRequest("Wrong credentials");
            return Ok(GenerateAndSetToken(new DtoInputToken
                { username = authResult.username, userType = authResult.usertype }));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputPassenger> Registration(DtoInputCreatePassenger dto)
    {
        var registrationResult = _useCaseCreatePassenger.Execute(dto);
        GenerateAndSetToken(new DtoInputToken
            { username = registrationResult.Username, userType = registrationResult.UserType });
        return Ok(registrationResult);
    }

    [AllowAnonymous]
    [HttpPost("generationToken")]
    public DtoOutputToken GenerateAndSetToken(DtoInputToken dto)
    {
        var token = _tokenService.BuildToken(_configuration["JWT:Key"], _configuration["JWT:Issuer"], dto);
        HttpContext.Response.Cookies.Append("WayMateToken", token, new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            MaxAge = TimeSpan.FromHours(2),
            IsEssential = true
        });

        return new DtoOutputToken
        {
            token = token
        };
    }

    [HttpGet(
        "registration/by-email/{email:regex(^[[a-z0-9]]+(?:.[[a-z0-9]]+)*@(?:[[a-z0-9]](?:[[a-z0-9-]]*[[a-z0-9]])?.)+[[a-z0-9]](?:[[a-z0-9-]]*[[a-z0-9]])?$)}")]
    [ProducesResponseTypeAttribute(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DtoOutputRegistration> FetchByEmail(string email)
    {
        return _useCaseRegistrationEmail.Execute(email);
    }

    [HttpGet("registration/by-username/{username:regex(^[[a-zA-Z0-9_-]]{{5,20}}$)}")]
    [ProducesResponseTypeAttribute(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DtoOutputRegistration> FetchByUsername(string username)
    {
        return _useCaseRegistrationUsername.Execute(username);
    }


    [HttpGet("IsConnected")]
    [Authorize]
    public ActionResult IsConnected()
    {
        return Ok();
    }

    [HttpGet("TestConnectionPassenger")]
    [Authorize(Roles = "Passenger")]
    public ActionResult TestConnectionPassenger()
    {
        var identityName = User.Identity?.Name;
        Console.Write(identityName);
        return Ok(new
        {
            text = "Ok"
        });
    }

    [HttpGet("TestConnectionDriver")]
    [Authorize(Roles = "Driver")]
    public ActionResult TestConnectionDriver()
    {
        var identityName = User.Identity?.Name;
        Console.Write(identityName);
        return Ok(new
        {
            text = "Ok"
        });
    }

    [HttpGet("TestConnectionAdmin")]
    [Authorize(Roles = "Admin")]
    public ActionResult TestConnectionAdmin()
    {
        var identityName = User.Identity?.Name;
        Console.Write(identityName);
        return Ok(new
        {
            text = "Ok"
        });
    }


    [HttpGet("getUsername")]
    [Authorize]
    public ActionResult<DtoOutputUsername> GetUsername()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null) return BadRequest();

        var usernameClaim = identity.FindFirst(ClaimTypes.Name);
        if (usernameClaim == null) return BadRequest(new { message = "Username not found in the token." });

        return Ok(new DtoOutputUsername { username = usernameClaim?.Value });
    }


    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("WayMateToken", new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            MaxAge = TimeSpan.FromHours(-1),
            IsEssential = true
        });

        return Ok(new { message = "Logout successful" });
    }
}
