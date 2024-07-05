﻿using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Trip;

public class TripRepository : ITripRepository
{
    private readonly WaymateContext _context;

    public TripRepository(WaymateContext context)
    {
        _context = context;
    }

    public IEnumerable<DbTrip> FetchAll()
    {
        return _context.Trip.ToList();
    }

    public DbTrip Create(int idDriver, bool smoke, float price, bool luggage, bool petFriendly, DateTime date,
        string driverMessage, bool airConditioning, string cityStartingPoint, string cityDestination, string plateNumber,
        string brand, string model)
    {
        var trip = new DbTrip { IdDriver = idDriver, Smoke = smoke, Price = price, Luggage = luggage, 
            PetFriendly = petFriendly, Date = date, DriverMessage = driverMessage, 
            AirConditioning = airConditioning, CityStartingPoint = cityStartingPoint, CityDestination = cityDestination, PlateNumber = plateNumber};
        _context.Trip.Add(trip);
        _context.SaveChanges();
        return trip;
    }

    public DbTrip FetchById(int id)
    {
        var trip = _context.Trip.FirstOrDefault(t => t.Id == id);
        
        if(trip == null) throw new KeyNotFoundException($"Trip with id{id} has not been found");

        return trip;
    }

    public bool Delete(int id)
    {
        var tripToDelete = _context.Trip.FirstOrDefault(t => t.Id == id);

        if (tripToDelete == null)
        {
            throw new KeyNotFoundException($"Trip with id {id} has not been found");
        }

        _context.Trip.Remove(tripToDelete);
        _context.SaveChanges();

        return true;
    }

    public bool Update(int id, int idDriver, bool smoke, float price, bool luggage, bool petFriendly, DateTime date,
        string driverMessage, bool airConditioning, string cityStartingPoint, string cityDestination, string plateNumber,
        string brand, string model)
    {
        var tripToUpdate = _context.Trip.FirstOrDefault(t => t.Id == id);
        if(tripToUpdate == null) return false;

        tripToUpdate.IdDriver = idDriver;
        tripToUpdate.Smoke = smoke;
        tripToUpdate.Price = price;
        tripToUpdate.Luggage = luggage;
        tripToUpdate.PetFriendly = petFriendly;
        tripToUpdate.Date = date;
        tripToUpdate.DriverMessage = driverMessage;
        tripToUpdate.AirConditioning = airConditioning;
        tripToUpdate.CityStartingPoint = cityStartingPoint;
        tripToUpdate.CityDestination = cityDestination;
        tripToUpdate.PlateNumber = plateNumber;

        _context.SaveChanges();
        return true;
    }
}