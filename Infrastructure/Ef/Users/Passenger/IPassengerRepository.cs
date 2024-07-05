using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Users.Passenger;

public interface IPassengerRepository
{
    DbUser CreatePassenger(string username, string password, string email, DateTime birthdate, bool isbanned, string phoneNumber,
        string lastName, string firstName, string gender, string city);
    
    DbUser UpdatePassenger(int id, string username, string password, string email, DateTime birthdate, bool isbanned, string phoneNumber,
        string lastName, string firstName, string gender, string city);
}