using Application.UseCases.Trip.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseFetchTripById : IUseCaseParameterizeQuery<DtoOutputTrip, int>
{
    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchTripById(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public DtoOutputTrip Execute(int id)
    {
        var dbTrip = _tripRepository.FetchById(id);
        return _mapper.Map<DtoOutputTrip>(dbTrip);
    }
}