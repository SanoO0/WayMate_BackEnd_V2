using Application.UseCases.Trip.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseFetchTripByFilter: IUseCaseParameterizeQuery<IEnumerable<DtoOutputTrip>, int, int, string>
{
    private readonly IMapper _mapper;
    private readonly ITripRepository _tripRepository;

    public UseCaseFetchTripByFilter(IMapper mapper, ITripRepository tripRepository)
    {
        _mapper = mapper;
        _tripRepository = tripRepository;
    }

    public IEnumerable<DtoOutputTrip> Execute(int connectedUserId, int userCount, string searchValue)
    {
        var trips = _tripRepository
            .FetchTripByFilter(connectedUserId, searchValue, userCount)
            .Select(trip =>
            {
                var dtoTrip = _mapper.Map<DtoOutputTrip>(trip);
                return dtoTrip;
            })
            .ToList();

        return trips;
    }
}