using Application.UseCases.Trip.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseUpdateTrip : IUseCaseParameterizeQuery<bool, DtoInputUpdateTrip, int, int>
{
    private readonly ITripRepository _tripRepository;

    public UseCaseUpdateTrip(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public bool Execute(DtoInputUpdateTrip update, int id, int idDriver)
    {
        return _tripRepository.Update(id,idDriver, update.Smoke, update.Price,
            update.Luggage, update.PetFriendly, update.Date, update.DriverMessage,
            update.AirConditioning, update.CityStartingPoint, update.CityDestination,
            update.PlateNumber,update.Brand,update.Model);
    }
}