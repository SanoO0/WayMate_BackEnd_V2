namespace Infrastructure.Ef.Authentication;

public class PasswordHasher : IPasswordHasher {
    private const int Cost = 12;


    public string HashPwd(string pwd) {
        return BCrypt.Net.BCrypt.HashPassword(pwd, workFactor: Cost);
    }

    public bool VerifyPwd(string hashedPwd, string inputPwd) {
        if (string.IsNullOrWhiteSpace(hashedPwd) || string.IsNullOrWhiteSpace(inputPwd)) return false;
        return BCrypt.Net.BCrypt.Verify(inputPwd, hashedPwd);
    }
}