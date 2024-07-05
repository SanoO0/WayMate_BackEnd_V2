using Application.UseCases.Utils;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Users.User;

public class UseCaseDeleteUser : IUseCaseParameterizeQuery<bool, int>
{
    private readonly IUserRepository _userRepository;

    public UseCaseDeleteUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Execute(int id)
    {
        return _userRepository.Delete(id);
    }
}