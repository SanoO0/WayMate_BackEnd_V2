using Application.UseCases.Booking.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Booking;

namespace Application.UseCases.Booking;

public class UseCaseFetchBookingByFilter : IUseCaseParameterizeQuery<IEnumerable<DtoOutputBooking>, int>
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;


    public UseCaseFetchBookingByFilter(IMapper mapper, IBookingRepository bookingRepository)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }

    public IEnumerable<DtoOutputBooking> Execute(int connectedUserId)
    {
        var bookings = _bookingRepository
            .FetchBookingByFilter(connectedUserId)
            .Select(booking =>
            {
                var dtoBooking = _mapper.Map<DtoOutputBooking>(booking);
                return dtoBooking;
            })
            .ToList();

        return bookings;
    }
}