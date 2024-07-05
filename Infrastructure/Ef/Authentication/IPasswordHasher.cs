namespace Infrastructure.Ef.Authentication;

public interface IPasswordHasher
{
    string HashPwd(string pwd);
    bool VerifyPwd(string pwdHash, string inputPwd);
}