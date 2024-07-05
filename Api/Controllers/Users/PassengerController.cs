using Application.UseCases.Users.Passenger;
using Application.UseCases.Users.Passenger.Dto;
using Application.UseCases.Users.User;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users; 

[ApiController]
[Route("api/v1/Passenger")]
public class PassengerController : ControllerBase{
    private readonly UseCaseCreatePassenger _useCaseCreatePassenger;
    private readonly UseCaseUpdatePassenger _useCaseUpdatePassenger;
    private readonly UseCaseFetchUserById _useCaseFetchUserById;
    private readonly UseCaseFetchAllUser _useCaseFetchAllUser;
    public PassengerController(UseCaseCreatePassenger useCaseCreatePassenger, UseCaseUpdatePassenger useCaseUpdatePassenger, UseCaseFetchUserById useCaseFetchUserById, UseCaseFetchAllUser useCaseFetchAllUser) {
        _useCaseCreatePassenger = useCaseCreatePassenger;
        _useCaseUpdatePassenger = useCaseUpdatePassenger;
        _useCaseFetchUserById = useCaseFetchUserById;
        _useCaseFetchAllUser = useCaseFetchAllUser;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputPassenger>> FetchAll()
    {
        var userResult = _useCaseFetchAllUser.Execute()
            .Where(passenger => passenger.UserType == UserType.Passenger.ToString())
            .Select(passenger => new DtoOutputPassenger()
            {
                Id = passenger.Id,
                UserType = passenger.UserType,
                Username = passenger.Username,
                Password = passenger.Password,
                Email = passenger.Email,
                Birthdate = passenger.Birthdate,
                IsBanned = passenger.IsBanned,
                PhoneNumber = passenger.PhoneNumber,
                LastName = passenger.LastName,
                FirstName = passenger.FirstName,
                Gender = passenger.Gender,
                City = passenger.City
                
            })
            .ToList();

        return userResult;
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputPassenger> FetchById(int id) {
        try {
            var passenger = _useCaseFetchUserById.Execute(id);

            if (passenger.UserType != UserType.Passenger.ToString())
                return NotFound($"User with ID {id} is not an passenger.");
            
            return new DtoOutputPassenger() {
                Id = passenger.Id,
                UserType = passenger.UserType,
                Username = passenger.Username,
                Password = passenger.Password,
                Email = passenger.Email,
                Birthdate = passenger.Birthdate,
                IsBanned = passenger.IsBanned,
                PhoneNumber = passenger.PhoneNumber,
                LastName = passenger.LastName,
                FirstName = passenger.FirstName,
                Gender = passenger.Gender,
                City = passenger.City
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
    public ActionResult<DtoOutputPassenger> Create(DtoInputCreatePassenger dto) {
        var output = _useCaseCreatePassenger.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(int id, [FromBody] DtoInputUpdatePassenger dto) {
        dto.Id = id;
        var output = _useCaseUpdatePassenger.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
    }
}