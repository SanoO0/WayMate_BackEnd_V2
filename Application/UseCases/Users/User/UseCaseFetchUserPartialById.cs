using Application.UseCases.Users.User.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserPartialById : IUseCaseParameterizeQuery<DtoOutputPartialUser, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchUserPartialById(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputPartialUser Execute(int id)
    {
        var dbUser = _userRepository.FetchByIdPartial(id);
        return _mapper.Map<DtoOutputPartialUser>(dbUser);
    }
}