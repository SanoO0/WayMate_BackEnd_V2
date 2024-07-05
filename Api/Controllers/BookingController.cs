using Application.UseCases.Booking;
using Application.UseCases.Booking.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/booking")]
public class BookingController: ControllerBase
{
    private readonly UseCaseCreateBooking _useCaseCreateBooking;
    private readonly UseCaseFetchAllBooking _useCaseFetchAllBooking;
    private readonly UseCaseFetchBookingById _useCaseFetchBookingById;
    private readonly UseCaseDeleteBooking _useCaseDeleteBooking;
    


    public BookingController(UseCaseCreateBooking useCaseCreateBooking, UseCaseFetchAllBooking useCaseFetchAllBooking, UseCaseFetchBookingById useCaseFetchBookingById, UseCaseDeleteBooking useCaseDeleteBooking)
    {
        _useCaseCreateBooking = useCaseCreateBooking;
        _useCaseFetchAllBooking = useCaseFetchAllBooking;
        _useCaseFetchBookingById = useCaseFetchBookingById;
        _useCaseDeleteBooking = useCaseDeleteBooking;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputBooking>> FetchAll()
    {
        return Ok(_useCaseFetchAllBooking.Execute());
    }
    
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputBooking> FetchById(int id)
    {
        try
        {
            return _useCaseFetchBookingById.Execute(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputBooking> Create(DtoInputCreateBooking dto)
    {
        var output = _useCaseCreateBooking.Execute(dto);
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
        if(_useCaseDeleteBooking.Execute(id)) return NoContent();
        return NotFound();
    }
}