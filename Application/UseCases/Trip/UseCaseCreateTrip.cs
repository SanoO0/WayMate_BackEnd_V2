using Application.UseCases.Trip.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseCreateTrip : IUseCaseParameterizeQuery<DtoOutputTrip, DtoInputCreateTrip, int>
{
    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateTrip(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public DtoOutputTrip Execute(DtoInputCreateTrip input, int idDriver)
    {
        var dbTrip = _tripRepository.Create(
            idDriver,
            input.Smoke,
            input.Price,
            input.Luggage,
            input.PetFriendly,
            input.Date,
            input.DriverMessage,
            input.AirConditioning,
            input.CityStartingPoint,
            input.CityDestination,
            input.PlateNumber,
            input.Brand,
            input.Model
            
        );
        return _mapper.Map<DtoOutputTrip>(dbTrip);
    }
}