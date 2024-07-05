using Application.UseCases.Users.Driver;
using Application.UseCases.Users.Driver.Dto;
using Application.UseCases.Users.Passenger.Dto;
using Application.UseCases.Users.User;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users; 

[ApiController]
[Route("api/v1/Driver")]
public class DriverController : ControllerBase{
    private readonly UseCaseCreateDriver _useCaseCreateDriver;
    private readonly UseCaseUpdateDriver _useCaseUpdateDriver;
    private readonly UseCaseFetchUserById _useCaseFetchUserById;
    private readonly UseCaseFetchAllUser _useCaseFetchAllUser;

    public DriverController(UseCaseCreateDriver useCaseCreateDriver, UseCaseUpdateDriver useCaseUpdateDriver, UseCaseFetchUserById useCaseFetchUserById, UseCaseFetchAllUser useCaseFetchAllUser) {
        _useCaseCreateDriver = useCaseCreateDriver;
        _useCaseUpdateDriver = useCaseUpdateDriver;
        _useCaseFetchUserById = useCaseFetchUserById;
        _useCaseFetchAllUser = useCaseFetchAllUser;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputDriver>> FetchAll()
    {
        var userResult = _useCaseFetchAllUser.Execute()
            .Where(driver => driver.UserType == UserType.Driver.ToString())
            .Select(driver => new DtoOutputDriver()
            {
                Id = driver.Id,
                UserType = driver.UserType,
                Username = driver.Username,
                Password = driver.Password,
                Email = driver.Email,
                Birthdate = driver.Birthdate,
                IsBanned = driver.IsBanned,
                PhoneNumber = driver.PhoneNumber,
                LastName = driver.LastName,
                FirstName = driver.FirstName,
                Gender = driver.Gender,
                City = driver.City
                
            })
            .ToList();

        return userResult;
    }
 
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputDriver> FetchById(int id) {
        try {
            var driver = _useCaseFetchUserById.Execute(id);

            if (driver.UserType != UserType.Driver.ToString())
                return NotFound($"User with ID {id} is not an driver.");
            
            return new DtoOutputDriver() {
                Id = driver.Id,
                UserType = driver.UserType,
                Username = driver.Username,
                Password = driver.Password,
                Email = driver.Email,
                Birthdate = driver.Birthdate,
                IsBanned = driver.IsBanned,
                PhoneNumber = driver.PhoneNumber,
                LastName = driver.LastName,
                FirstName = driver.FirstName,
                Gender = driver.Gender,
                City = driver.City
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
    public ActionResult<DtoOutputDriver> Create(DtoInputCreateDriver dto) {
        var output = _useCaseCreateDriver.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(int id, [FromBody] DtoInputUpdateDriver dto) {
        dto.Id = id;
        var output = _useCaseUpdateDriver.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
    }
}