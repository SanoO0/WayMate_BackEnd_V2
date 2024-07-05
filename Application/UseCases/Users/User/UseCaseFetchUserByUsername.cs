using Application.UseCases.Users.User.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserByUsername : IUseCaseParameterizeQuery<DtoOutputUser, string> {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchUserByUsername(IUserRepository userRepository, IMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputUser Execute(string username) {
        var dbUser = _userRepository.FetchByUsernameDbUser(username);

        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}