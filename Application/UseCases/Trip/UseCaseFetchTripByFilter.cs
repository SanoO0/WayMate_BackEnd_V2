﻿using Application.UseCases.Trip.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseFetchTripByFilter: IUseCaseParameterizeQuery<IEnumerable<DtoOutputTrip>, int, int>
{
    private readonly IMapper _mapper;
    private readonly ITripRepository _tripRepository;

    public UseCaseFetchTripByFilter(IMapper mapper, ITripRepository tripRepository)
    {
        _mapper = mapper;
        _tripRepository = tripRepository;
    }

    public IEnumerable<DtoOutputTrip> Execute(int connectedUserId, int userCount)
    {
        var trips = _tripRepository
            .FetchTripByFilter(connectedUserId, userCount)
            .Select(trip =>
            {
                var dtoTrip = _mapper.Map<DtoOutputTrip>(trip);
                return dtoTrip;
            })
            .ToList();

        return trips;
    }
}