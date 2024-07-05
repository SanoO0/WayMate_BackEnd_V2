using Infrastructure.Ef.Trip;

namespace Application.UseCases.Trip;

public class UseCaseDeleteTrip
{
    private readonly ITripRepository _tripRepository;

    public UseCaseDeleteTrip(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public bool Execute(int id)
    {
        return _tripRepository.Delete(id);
    }
}