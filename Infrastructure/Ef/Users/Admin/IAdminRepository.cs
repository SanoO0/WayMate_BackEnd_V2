using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Users.Admin;

public interface IAdminRepository {
    DbUser CreateAdmin(string username, string password, string email, DateTime birthdate, bool isBanned,
        string phoneNumber);

    DbUser UpdateAdmin(int id,string username, string password, string email, DateTime birthDate, bool isBanned,
        string phoneNumber);
}