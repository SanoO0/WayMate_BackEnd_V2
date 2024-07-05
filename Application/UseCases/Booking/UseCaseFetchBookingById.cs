using Application.UseCases.Booking.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Booking;

namespace Application.UseCases.Booking;

public class UseCaseFetchBookingById : IUseCaseParameterizeQuery<DtoOutputBooking, int>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchBookingById(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public DtoOutputBooking Execute(int id)
    {
        var dbBooking = _bookingRepository.FetchById(id);
        return _mapper.Map<DtoOutputBooking>(dbBooking);
    }
}