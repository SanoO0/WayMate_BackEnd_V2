using Application.UseCases.Users.User.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.User;

namespace Application.UseCases.Users.User;

public class UseCaseCreateUser : IUseCaseWriter<DtoOutputUser, DtoInputCreateUser> {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateUser(IUserRepository userRepository, IMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputUser Execute(DtoInputCreateUser input) {
        var dbUser = _userRepository.Create(input.Username, input.Password, input.Email, input.Birthdate,
            input.IsBanned, input.PhoneNumber, input.LastName, input.FirstName, input.Gender, input.City);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}