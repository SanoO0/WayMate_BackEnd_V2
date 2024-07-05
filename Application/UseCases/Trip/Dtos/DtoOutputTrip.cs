namespace Application.UseCases.Trip.Dtos;

public class DtoOutputTrip
{
    public int Id { get; set; }
    public int IdDriver { get; set; }
    public bool Smoke { get; set; }
    public float Price { get; set; }
    public bool Luggage { get; set; }
    public bool PetFriendly { get; set; }
    public DateTime Date { get; set; }
    public string DriverMessage { get; set; }
    public bool AirConditioning { get; set; }
    public string CityStartingPoint { get; set; }
    public string CityDestination { get; set; }
    public string PlateNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
}