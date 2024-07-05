using Application.UseCases.Trip.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseFetchAllTrip : IUseCaseQuery<IEnumerable<DtoOutputTrip>>
{
    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchAllTrip(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public IEnumerable<DtoOutputTrip> Execute()
    {
        var dbTrip = _tripRepository.FetchAll();
        return _mapper.Map<IEnumerable<DtoOutputTrip>>(dbTrip);
    }
}