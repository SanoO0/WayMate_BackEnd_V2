using Application.UseCases.Users.Admin;
using Application.UseCases.Users.Admin.Dto;
using Application.UseCases.Users.User;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Api.Controllers.Users;

[ApiController]
[Route("api/v1/admin")]
public class AdminController : ControllerBase {
    private readonly UseCaseCreateAdmin _useCaseCreateAdmin;
    private readonly UseCaseFetchUserById _useCaseFetchUserById;
    private readonly UseCaseUpdateAdmin _useCaseUpdateAdmin;
    private readonly UseCaseFetchAllUser _useCaseFetchAllUser;

    public AdminController(UseCaseCreateAdmin useCaseCreateAdmin,
        UseCaseFetchUserById useCaseFetchUserById, UseCaseUpdateAdmin useCaseUpdateAdmin, UseCaseFetchAllUser useCaseFetchAllUser) {
        _useCaseCreateAdmin = useCaseCreateAdmin;
        _useCaseFetchUserById = useCaseFetchUserById;
        _useCaseUpdateAdmin = useCaseUpdateAdmin;
        _useCaseFetchAllUser = useCaseFetchAllUser;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputAdmin>> FetchAll()
    {
        var userResult = _useCaseFetchAllUser.Execute()
            .Where(admin => admin.UserType == UserType.Admin.ToString())
            .Select(admin => new DtoOutputAdmin
            {
                Id = admin.Id,
                UserType = admin.UserType,
                Username = admin.Username,
                Password = admin.Password,
                Email = admin.Email,
                Birthdate = admin.Birthdate,
                IsBanned = admin.IsBanned,
                PhoneNumber = admin.PhoneNumber
            })
            .ToList();

        return userResult;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputAdmin> FetchById(int id) {
        try {
            var userResult = _useCaseFetchUserById.Execute(id);

            if (userResult.UserType != UserType.Admin.ToString())
                return NotFound($"User with ID {id} is not an admin.");
            
            return new DtoOutputAdmin {
                Id = userResult.Id,
                UserType = userResult.UserType,
                Username = userResult.Username,
                Password = userResult.Password,
                Email = userResult.Email,
                Birthdate = userResult.Birthdate,
                IsBanned = userResult.IsBanned,
                PhoneNumber = userResult.PhoneNumber
            };
        }
        catch (KeyNotFoundException e) {
            return NotFound(new {
                e.Message
            });
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputAdmin> Create(DtoInputCreateAdmin dto) {
        var output = _useCaseCreateAdmin.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(int id, [FromBody] DtoInputUpdateAdmin dto) {
        dto.Id = id;
        var output = _useCaseUpdateAdmin.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
    }
}