using Application.UseCases.Booking.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Booking;

namespace Application.UseCases.Booking;

public class UseCaseCreateBooking : IUseCaseWriter<DtoOutputBooking, DtoInputCreateBooking>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateBooking(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public DtoOutputBooking Execute(DtoInputCreateBooking input)
    {
        var dbBooking = _bookingRepository.Create(input.Date, input.ReservedSeats, input.IdPassenger, input.IdTrip);
        return _mapper.Map<DtoOutputBooking>(dbBooking);
    }
}