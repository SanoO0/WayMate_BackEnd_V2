namespace Infrastructure.Ef.DbEntities;

public class DbBooking
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int ReservedSeats { get; set; }
    public int IdPassenger { get; set; }
    public int IdTrip { get; set; }
}