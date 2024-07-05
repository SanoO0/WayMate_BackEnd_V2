namespace Domain.Entities;

public class Trip
{
    private User _driver;
    public bool Smoker { get; set; }
    public double PriceKm { get; set; }
    public bool Luggage { get; set; }
    public bool PetFriendly { get; set; }
    public string TripDate { get; set; }
    public string DriverMessage { get; set; }
    public bool AirConditioning { get; set; }
    public string StartCity { get; set; }
    public string DestinationCity { get; set; }
    public string PlateNumber  { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
}