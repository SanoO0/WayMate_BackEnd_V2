using Domain.Enums;
using Infrastructure.Ef.Authentication;
using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Users.User;

public class UserRepository : IUserRepository {
    private readonly WaymateContext _context;

    public UserRepository(WaymateContext context) {
        _context = context;
    }

    public IEnumerable<DbUser> FetchAll() {
        return _context.Users.ToList();
    }

    public DbUser FetchById(int id) {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) throw new KeyNotFoundException($"User with id {id} has not been found");
        return user;
    }

    public DbUser FetchByIdPartial(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) throw new KeyNotFoundException($"User with id {id} has not been found");
        return user;
    }

    public DbUser FetchByEmail(string email) {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) throw new KeyNotFoundException($"User with email {email} has not been found");
        return user;
    }

    public DbUser Create(string username, string password, string email, DateTime birthdate, bool isbanned, string phoneNumber,
        string lastName, string firstName, string gender, string city) {
        var user = new DbUser {
            Username = username,
            UserType = UserType.User.ToString(),
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
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public DbUser Update(int id, string username, string password, string email, DateTime birthdate, bool isbanned,
        string phoneNumber, string lastName, string firstName, string gender, string city) {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userToUpdate == null) throw new KeyNotFoundException($"User with id {id} has not been found");
        
        userToUpdate.Username = username;
        userToUpdate.UserType = UserType.User.ToString();
        userToUpdate.Password = PasswordHasher.HashPassword(password);
        userToUpdate.Email = email;
        userToUpdate.BirthDate = birthdate;
        userToUpdate.IsBanned = isbanned;
        userToUpdate.PhoneNumber = phoneNumber;
        userToUpdate.LastName = lastName;
        userToUpdate.FirstName = firstName;
        userToUpdate.Gender = gender;
        userToUpdate.City = city;

        _context.SaveChanges();
        return userToUpdate;
    }

    public DbUser FetchByUsernameDbUser(string username) {
        var user = _context.Users.FirstOrDefault(a => a.Username == username);
        if (user == null) throw new KeyNotFoundException($"User with username {username} has not been found.");
        return user;
    }

    public bool Delete(int id) {
        var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);
        if (userToDelete == null) throw new KeyNotFoundException($"User with id {id} has not been found");

        _context.Users.Remove(userToDelete);
        _context.SaveChanges();
        return true;
    }

    public bool FetchByUsername(string username) {
        var user = _context.Users.FirstOrDefault(a => a.Username == username);

        if (user == null)  return false;

        return true;
    }

    public bool FetchByEmailBool(string email) {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null) return false;

        return true;
    }
}