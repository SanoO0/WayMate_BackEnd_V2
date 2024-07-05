using Domain.Enums;
using Infrastructure.Ef.Authentication;
using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Users.Driver;

public class DriverRepository : IDriverRepository
{
    private readonly WaymateContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public DriverRepository(WaymateContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public DbUser CreateDriver(string username, string password, string email, DateTime birthdate, bool isbanned,
        string phoneNumber, string lastName, string firstName, string gender, string city)
    {
        var newDriver = new DbUser {
            Username = username,
            UserType = UserType.Driver.ToString(),
            Password = _passwordHasher.HashPwd(password),
            Email = email,
            BirthDate = birthdate,
            IsBanned = isbanned,
            PhoneNumber = phoneNumber,
            LastName = lastName,
            FirstName = firstName,
            Gender = gender,
            City = city
        };
        _context.Users.Add(newDriver);
        _context.SaveChanges();
        return newDriver;
    }

    public DbUser UpdateDriver(int id, string username, string password, string email, DateTime birthdate, bool isbanned,
        string phoneNumber, string lastName, string firstName, string gender, string city)
    {
        var driverToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);

        if (driverToUpdate == null) throw new KeyNotFoundException($"Driver with id {id} has not been found.");

        driverToUpdate.Username = username;
        driverToUpdate.Password = password;
        driverToUpdate.Email = email;
        driverToUpdate.BirthDate = birthdate;
        driverToUpdate.IsBanned = isbanned;
        driverToUpdate.PhoneNumber = phoneNumber;
        driverToUpdate.LastName = lastName;
        driverToUpdate.FirstName = firstName;
        driverToUpdate.Gender = gender;
        driverToUpdate.City = city;

        _context.SaveChanges();
        return driverToUpdate;
    }
}