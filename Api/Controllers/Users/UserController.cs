using Application.Services.TokenJWT;
using Application.UseCases.Users.Admin;
using Application.UseCases.Users.Driver.Dto;
using Application.UseCases.Users.User;
using Application.UseCases.Users.User.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase {
    private readonly UseCaseFetchAllUser _useCaseFetchAllUser;
    private readonly UseCaseFetchUserById _useCaseFetchUserById;
    private readonly UseCaseFetchUserByEmail _userCaseFetchUserByEmail;
    private readonly UseCaseDeleteUser _useCaseDeleteUser;
    private readonly UseCaseUpdateUser _useCaseUpdateUser;
    private readonly UseCaseCreateUser _useCaseCreateUser;
    private readonly UseCaseFetchUserByUsername _useCaseFetchUserByUsername;
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    public UserController(
        UseCaseFetchAllUser useCaseFetchAllUser,
        UseCaseFetchUserById useCaseFetchUserById,
        UseCaseFetchUserByEmail userCaseFetchUserByEmail,
        UseCaseDeleteUser useCaseDeleteUser,
        UseCaseUpdateUser useCaseUpdateUser, UseCaseCreateAdmin useCaseCreateAdmin, UseCaseCreateUser useCaseCreateUser, 
        UseCaseFetchUserByUsername useCaseFetchUserByUsername, TokenService tokenService, IConfiguration configuration) {
        _useCaseFetchAllUser = useCaseFetchAllUser;
        _useCaseFetchUserById = useCaseFetchUserById;
        _userCaseFetchUserByEmail = userCaseFetchUserByEmail;
        _useCaseDeleteUser = useCaseDeleteUser;
        _useCaseUpdateUser = useCaseUpdateUser;
        _useCaseCreateUser = useCaseCreateUser;
        _useCaseFetchUserByUsername = useCaseFetchUserByUsername;
        _tokenService = tokenService;
        _configuration = configuration;
    }
    
    private (string Id, string UserType) GetConnectedUserStatus()
    {
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!;
        return _tokenService.GetAuthCookieData(token);
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputUser>> FetchAll() {
        return Ok(_useCaseFetchAllUser.Execute());
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchById(int id) {
        try {
            return  Ok(_useCaseFetchUserById.Execute(id));
        }
        catch (KeyNotFoundException e) {
            return NotFound(new {
                e.Message
            });
        }
    }
    
    [HttpGet]
    [Route("getConnectedUserData")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchById() {
        try {
            var data = GetConnectedUserStatus();
            return  Ok(_useCaseFetchUserById.Execute(Int32.Parse(data.Id)));
        }
        catch (KeyNotFoundException e) {
            return NotFound(new {
                e.Message
            });
        }
    }

    [HttpGet]
    [Route("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputUser> FetchByEmail(string email) {
        try {
            var result = _userCaseFetchUserByEmail.Execute(email);
            return result != null ? Ok(result) : NotFound(new { Message = "User not found" });
    
        }
        catch (KeyNotFoundException e) {
            return NotFound(new {
                e.Message
            });
        }
    }

    [HttpGet("GetByUsername/{username:regex(^[[a-zA-Z0-9_-]]{{5,20}}$)}")]
    [ProducesResponseTypeAttribute(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DtoOutputUser> FetchByUsername(string username) {
        try {
            return Ok(_useCaseFetchUserByUsername.Execute(username));
        }
        catch (KeyNotFoundException e) {
            return NotFound(new {
                e.Message
            });
        }
        
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id) {
        if (_useCaseDeleteUser.Execute(id)) return NoContent();
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputUser> Create(DtoInputCreateUser dto ) {
        var output = _useCaseCreateUser.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new {id = output.Id},
            output
            );
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(int id, [FromBody] DtoInputUpdateUser dto) {
        dto.Id = id;
        var output = _useCaseUpdateUser.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new {id = output.Id},
            output
            );
    }
}