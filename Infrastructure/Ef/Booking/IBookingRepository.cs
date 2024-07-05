using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Booking;

public interface IBookingRepository
{
    IEnumerable<DbBooking> FetchAll();

    DbBooking FetchById(int id);

    DbBooking Create(DateTime date, int reservedSeats, int idPassenger, int idTrip);
    
    bool Delete(int id);
}