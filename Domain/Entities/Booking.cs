namespace Domain.Entities;

public class Booking
{
    public string BookingDate { get; set; }
    public int NbBookedSeats { get; set; }
    public Trip Trip { get; set; }
    public User User { get; set; }
}