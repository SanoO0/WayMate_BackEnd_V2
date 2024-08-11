﻿using System.Collections;
using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef.Trip;

public interface ITripRepository
{
    IEnumerable<DbTrip> FetchAll();
    DbTrip Create(int idDriver, bool smoke, float price , bool luggage, bool petFriendly, DateTime date, string driverMessage, bool airConditioning, string cityStartingPoint, string cityDestination, string plateNumber, string brand, string model);
    DbTrip FetchById(int id);
    bool Delete(int id);
    bool Update(int id, int idDriver, bool smoke, float price , bool luggage, bool petFriendly, DateTime date, string driverMessage, bool airConditioning, string cityStartingPoint, string cityDestination, string plateNumber, string brand, string model);
    IEnumerable<DbTrip> FetchTripByFilter(int idDriver, int userCount);

    IEnumerable<DbTrip> FetchTripByFilterPassenger(int idPassenger, int userCount);
    IEnumerable<DbTrip> FetchTripByFilterCityAndDate(string? cityStartingPoint, string? cityDestination, DateTime? date);
}