using Application.UseCases.Users.User.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserById : IUseCaseParameterizeQuery<DtoOutputUser, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchUserById(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputUser Execute(int id)
    {
        var dbUser = _userRepository.FetchById(id);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}