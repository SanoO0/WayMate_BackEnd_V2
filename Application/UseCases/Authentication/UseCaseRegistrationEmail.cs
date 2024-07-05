using Application.UseCases.Authentication.Dtos;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Authentication;

public class UseCaseRegistrationEmail
{
    private readonly IUserRepository _userRepository;

    public UseCaseRegistrationEmail(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoOutputRegistration Execute(string email)
    {
        var dbUser = _userRepository.FetchByEmailBool(email);

        return new DtoOutputRegistration { IsInDb = dbUser };
    }
}