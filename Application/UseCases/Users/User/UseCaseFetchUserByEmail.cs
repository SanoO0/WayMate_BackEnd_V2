using Application.UseCases.Users.User.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserByEmail : IUseCaseParameterizeQuery<DtoOutputUser, string>{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchUserByEmail(IUserRepository userRepository, IMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputUser Execute(string email) {
        var dbUser = _userRepository.FetchByEmail(email);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}