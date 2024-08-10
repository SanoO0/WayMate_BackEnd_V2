using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Trip.Dtos;

public class DtoInputCreateTrip
{
    [Required] public bool Smoke { get; set; }
    [Required] public float Price { get; set; }
    [Required] public bool Luggage { get; set; }
    [Required] public bool PetFriendly { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public string DriverMessage { get; set; }
    [Required] public bool AirConditioning { get; set; }
    [Required] public string CityStartingPoint { get; set; }
    [Required] public string CityDestination { get; set; }
    [Required] public string PlateNumber { get; set; }
    [Required] public string Brand { get; set; }
    [Required] public string Model { get; set; }
}