namespace Infrastructure.Ef.Authentication;

public class PasswordHasher
{
    // Hashes a plain text password using BCrypt.
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
   
    // Verifies a plain text password against a hashed password using BCrypt.
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}