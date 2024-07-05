using Application.UseCases.Trip.Dtos;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseUpdateTrip
{
    private readonly ITripRepository _tripRepository;

    public UseCaseUpdateTrip(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public bool Execute(DtoInputUpdateTrip update)
    {
        return _tripRepository.Update(update.Id, update.IdDriver, update.Smoke, update.Price,
            update.Luggage, update.PetFriendly, update.Date, update.DriverMessage,
            update.AirConditioning, update.CityStartingPoint, update.CityDestination,
            update.PlateNumber,update.Brand,update.Model);
    }
}