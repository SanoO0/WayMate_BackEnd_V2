using Domain.Enums;
using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Users.Admin;

public class AdminRepository : IAdminRepository {
    private readonly WaymateContext _context;

    public AdminRepository(WaymateContext context)
    {
        _context = context;
    }
    
    public DbUser CreateAdmin(string username, string password, string email, DateTime birthdate, bool isBanned,
        string phoneNumber)
    {
        var newAdmin = new DbUser
        {
            Username = username,
            UserType = UserType.Admin.ToString(),
            Password = password,
            Email = email,
            BirthDate = birthdate,
            IsBanned = isBanned,
            PhoneNumber = phoneNumber
        };
        _context.Users.Add(newAdmin);
        _context.SaveChanges();
        return newAdmin;
    }

    public DbUser UpdateAdmin(int id, string username, string password, string email, DateTime birthDate, bool isBanned,
        string phoneNumber)
    {
        var adminToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);
        if (adminToUpdate == null) throw new KeyNotFoundException($"Admin with id {id} has not been found");

        adminToUpdate.Username = username;
        adminToUpdate.Password = password;
        adminToUpdate.Email = email;
        adminToUpdate.BirthDate = birthDate;
        adminToUpdate.IsBanned = isBanned;
        adminToUpdate.PhoneNumber = phoneNumber;

        _context.Users.Update(adminToUpdate);
        _context.SaveChanges();
        return adminToUpdate;
    }
}