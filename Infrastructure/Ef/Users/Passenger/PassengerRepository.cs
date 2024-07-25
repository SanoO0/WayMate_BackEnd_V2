using Domain.Enums;
using Infrastructure.Ef.Authentication;
using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Users.Passenger;

public class PassengerRepository : IPassengerRepository {
    private readonly WaymateContext _context;

    public PassengerRepository(WaymateContext context) {
        _context = context;
    }

    public DbUser CreatePassenger(string username, string password, string email, DateTime birthdate, bool isbanned,
        string phoneNumber, string lastName, string firstName, string gender, string city) {
        var newPassenger = new DbUser {
            Username = username,
            UserType = UserType.Passenger.ToString(),
            Password = PasswordHasher.HashPassword(password),
            Email = email,
            BirthDate = birthdate,
            IsBanned = isbanned,
            PhoneNumber = phoneNumber,
            LastName = lastName,
            FirstName = firstName,
            Gender = gender,
            City = city
        };
        _context.Users.Add(newPassenger);
        _context.SaveChanges();
        return newPassenger;
    }

    public DbUser UpdatePassenger(int id, string username, string password, string email, DateTime birthdate, bool isbanned,
        string phoneNumber, string lastName, string firstName, string gender, string city) {
        var passengerToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);

        if (passengerToUpdate == null) throw new KeyNotFoundException($"Passenger with id {id} has not been found.");

        passengerToUpdate.Username = username;
        passengerToUpdate.Password = PasswordHasher.HashPassword(password);
        passengerToUpdate.Email = email;
        passengerToUpdate.BirthDate = birthdate;
        passengerToUpdate.IsBanned = isbanned;
        passengerToUpdate.PhoneNumber = phoneNumber;
        passengerToUpdate.LastName = lastName;
        passengerToUpdate.FirstName = firstName;
        passengerToUpdate.Gender = gender;
        passengerToUpdate.City = city;

        _context.SaveChanges();

        return passengerToUpdate;
    }
}