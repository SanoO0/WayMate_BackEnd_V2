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

    public TripController(UseCaseFetchAllTrip useCaseFetchAllTrip, 
        UseCaseCreateTrip useCaseCreateTrip, 
        UseCaseFetchTripById useCaseFetchTripById, 
        UseCaseDeleteTrip useCaseDeleteTrip, 
        UseCaseUpdateTrip useCaseUpdateTrip)
    {
        _useCaseFetchAllTrip = useCaseFetchAllTrip;
        _useCaseCreateTrip = useCaseCreateTrip;
        _useCaseFetchTripById = useCaseFetchTripById;
        _useCaseDeleteTrip = useCaseDeleteTrip;
        _useCaseUpdateTrip = useCaseUpdateTrip;
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputTrip> Create(DtoInputCreateTrip dto)
    {
        var output = _useCaseCreateTrip.Execute(dto);
        return CreatedAtAction(
            nameof(FetchById),
            new { id = output.Id },
            output
        );
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
}