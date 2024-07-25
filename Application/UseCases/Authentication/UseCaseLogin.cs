using Application.UseCases.Authentication.Dtos;
using AutoMapper;
using Infrastructure.Ef.Authentication;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Authentication;

public class UseCaseLogin
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseLogin( IUserRepository userRepository, IMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputLogin Execute(string email) {
        var dbUser = _userRepository.FetchByEmail(email);
        return _mapper.Map<DtoOutputLogin>(dbUser);
    }
}