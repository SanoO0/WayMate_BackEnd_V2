using Application.UseCases.Users.Passenger.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.Passenger;

namespace Application.UseCases.Users.Passenger;

public class UseCaseUpdatePassenger : IUseCaseWriter<DtoOutputPassenger, DtoInputUpdatePassenger> {
    private readonly IPassengerRepository _passengerRepository;
    private readonly IMapper _mapper;

    public UseCaseUpdatePassenger(IPassengerRepository passengerRepository, IMapper mapper) {
        _passengerRepository = passengerRepository;
        _mapper = mapper;
    }

    public DtoOutputPassenger Execute(DtoInputUpdatePassenger input) {
        var dbPassenger = _passengerRepository.UpdatePassenger(input.Id,input.Username, input.Password, input.Email, 
            input.Birthdate, input.IsBanned, input.PhoneNumber, input.LastName, input.FirstName, input.Gender, input.City);
        return _mapper.Map<DtoOutputPassenger>(dbPassenger);
    }
}