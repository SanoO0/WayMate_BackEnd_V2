using Application.UseCases.Trip.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseFetchTripByFilterCityAndDate : IUseCaseParameterizeQuery<IEnumerable<DtoOutputTrip>, string?, string?, DateTime?>
{
    private readonly IMapper _mapper;
    private readonly ITripRepository _tripRepository;

    public UseCaseFetchTripByFilterCityAndDate(IMapper mapper, ITripRepository tripRepository)
    {
        _mapper = mapper;
        _tripRepository = tripRepository;
    }

    public IEnumerable<DtoOutputTrip> Execute( string? cityStartingPoint, string? cityDestination, DateTime? date)
    {
        var trips = _tripRepository
            .FetchTripByFilterCityAndDate(cityStartingPoint, cityDestination, date)
            .Select(trip =>
            {
                var dtoTrip = _mapper.Map<DtoOutputTrip>(trip);
                return dtoTrip;
            })
            .ToList();

        return trips;
    }
    
}