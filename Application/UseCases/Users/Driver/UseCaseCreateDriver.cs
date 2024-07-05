using Application.UseCases.Users.Driver.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.Driver;

namespace Application.UseCases.Users.Driver;

public class UseCaseCreateDriver : IUseCaseWriter<DtoOutputDriver, DtoInputCreateDriver>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateDriver(IDriverRepository driverRepository, IMapper mapper) {
        _driverRepository = driverRepository;
        _mapper = mapper;
    }

    public DtoOutputDriver Execute(DtoInputCreateDriver input) {
        var dbUser = _driverRepository.CreateDriver(input.Username, input.Password, input.Email, input.Birthdate,
            input.IsBanned, input.PhoneNumber, input.LastName, input.FirstName, input.Gender, input.City);
        return _mapper.Map<DtoOutputDriver>(dbUser);
    }
}