using Application.Services.TokenJWT;
using Application.UseCases.Trip;
using Application.UseCases.Trip.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/trip")]
public class TripController : ControllerBase
{
    private readonly UseCaseFetchAllTrip _useCaseFetchAllTrip;
    private readonly UseCaseCreateTrip _useCaseCreateTrip;
    private readonly UseCaseFetchTripById _useCaseFetchTripById;
    private readonly UseCaseDeleteTrip _useCaseDeleteTrip;
    private readonly UseCaseUpdateTrip _useCaseUpdateTrip;
    private readonly UseCaseFetchTripByFilter _useCaseFetchTripByFilter;
    private readonly UseCaseFetchTripByFilterPassenger _useCaseFetchTripByFilterPassenger;
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    public TripController(UseCaseFetchAllTrip useCaseFetchAllTrip, 
        IConfiguration configuration, TokenService tokenService,
        UseCaseCreateTrip useCaseCreateTrip, 
        UseCaseFetchTripById useCaseFetchTripById, 
        UseCaseDeleteTrip useCaseDeleteTrip, 
        UseCaseUpdateTrip useCaseUpdateTrip, 
        UseCaseFetchTripByFilter useCaseFetchTripByFilter, UseCaseFetchTripByFilterPassenger useCaseFetchTripByFilterPassenger)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        _useCaseFetchAllTrip = useCaseFetchAllTrip;
        _useCaseCreateTrip = useCaseCreateTrip;
        _useCaseFetchTripById = useCaseFetchTripById;
        _useCaseDeleteTrip = useCaseDeleteTrip;
        _useCaseUpdateTrip = useCaseUpdateTrip;
        _useCaseFetchTripByFilter = useCaseFetchTripByFilter;
        _useCaseFetchTripByFilterPassenger = useCaseFetchTripByFilterPassenger;
    }
    
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputTrip> FetchById(int id)
    {
        try
        {
            return _useCaseFetchTripById.Execute(id);
        }
        catch (Exception e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputTrip>> FetchAll()
    {
        return Ok(_useCaseFetchAllTrip.Execute());
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<DtoOutputTrip> Create([FromBody] DtoInputCreateTrip dto)
    {
        if (dto == null)
        {
            return BadRequest(new { error = "Invalid input: dto cannot be null" });
        }
        
        Console.WriteLine($"Received request with DTO: {Newtonsoft.Json.JsonConvert.SerializeObject(dto)}");
        
        // Optionnel : Ajoutez des validations sur les propriétés de dto
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Invalid data: " + ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage });
        }
        
        try
        {
            var data = GetConnectedUserStatus();
            var output = _useCaseCreateTrip.Execute( dto,Int32.Parse(data.Id));
            return CreatedAtAction(
                nameof(FetchById),
                new { id = data.Id },
                output);
        }
        catch (FormatException fe)
        {
            return BadRequest(new { error = "Invalid format: " + fe.Message });
        }
        catch (ArgumentException ae)
        {
            return BadRequest(new { error = "Invalid argument: " + ae.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(int id)
    {
        if(_useCaseDeleteTrip.Execute(id)) return NoContent();
        return NotFound();
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Update(int id, [FromBody] DtoInputUpdateTrip dto)
    {
        dto.Id = id;
        return _useCaseUpdateTrip.Execute(dto) ? NoContent() : NotFound();
    }
    
    private (string Id, string UserType) GetConnectedUserStatus()
    {
        var token = HttpContext.Request.Cookies[_configuration["JwtSettings:CookieName"]!]!;
        return _tokenService.GetAuthCookieData(token);
    }

    [HttpGet("tripByFilter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<DtoOutputTrip>> GetUserByFilter([FromQuery] int userCount)
    {
        try
        {
            var data = GetConnectedUserStatus();
            return Ok(_useCaseFetchTripByFilter.Execute(Int32.Parse(data.Id), userCount));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
    
    [HttpGet("tripByFilterPassenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<DtoOutputTrip>> GetUserByFilterPassenger([FromQuery] int userCount)
    {
        try
        {
            var data = GetConnectedUserStatus();
            return Ok(_useCaseFetchTripByFilterPassenger.Execute(Int32.Parse(data.Id), userCount));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message });
        }
    }
}