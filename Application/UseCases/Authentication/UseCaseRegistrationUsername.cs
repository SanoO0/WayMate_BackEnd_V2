using Application.UseCases.Authentication.Dtos;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Authentication;

public class UseCaseRegistrationUsername
{
    private readonly IUserRepository _userRepository;

    public UseCaseRegistrationUsername(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoOutputRegistration Execute(string username)
    {
        var dbUser = _userRepository.FetchByUsername(username);

        return new DtoOutputRegistration { IsInDb = dbUser };
    }
}