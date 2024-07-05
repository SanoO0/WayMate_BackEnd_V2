using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Booking.Dtos;

public class DtoInputCreateBooking
{
    [Required] public DateTime Date { get; set; }
    [Required] public int ReservedSeats { get; set; }
    [Required] public int IdPassenger { get; set; }
    [Required] public int IdTrip { get; set; }
}