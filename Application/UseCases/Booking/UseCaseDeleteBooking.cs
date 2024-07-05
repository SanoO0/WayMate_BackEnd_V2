using Application.UseCases.Utils;
using Infrastructure.Ef.Booking;

namespace Application.UseCases.Booking;

public class UseCaseDeleteBooking : IUseCaseParameterizeQuery<bool, int>
{
    private readonly IBookingRepository _bookingRepository;

    public UseCaseDeleteBooking(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public bool Execute(int id)
    {
        return _bookingRepository.Delete(id);
    }
}