using Application.UseCases.Booking.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Booking;

namespace Application.UseCases.Booking;

public class UseCaseFetchAllBooking : IUseCaseQuery<IEnumerable<DtoOutputBooking>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchAllBooking(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public IEnumerable<DtoOutputBooking> Execute()
    {
        var dbBooking = _bookingRepository.FetchAll();
        return _mapper.Map<IEnumerable<DtoOutputBooking>>(dbBooking);
    }
}