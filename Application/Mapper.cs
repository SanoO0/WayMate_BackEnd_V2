using Application.UseCases.Authentication.Dtos;
using Application.UseCases.Booking.Dtos;
using Application.UseCases.Trip.Dtos;
using Application.UseCases.Users.Admin.Dto;
using Application.UseCases.Users.Driver.Dto;
using Application.UseCases.Users.Passenger.Dto;
using Application.UseCases.Users.User.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Ef.DbEntities;

namespace Application;

public class Mapper : Profile
{
    public Mapper()
    {
        //Trip
        CreateMap<Trip, DtoOutputTrip>();
        CreateMap<DbTrip, DtoOutputTrip>();
        CreateMap<DbTrip, Trip>();
                
        //Booking
        CreateMap<Booking, DtoOutputBooking>();
        CreateMap<DbBooking, DtoOutputBooking>();
        CreateMap<DbBooking, Booking>();
        
        //User
        CreateMap<User, DtoOutputUser>();
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DbUser, User>();
        CreateMap<DbUser, DtoOutputUser>();
        
        
        //Admin
        CreateMap<DbUser, DtoOutputAdmin>();
        
        //Passenger
        CreateMap<DbUser, DtoOutputPassenger>();
        
        //Driver
        CreateMap<DbUser, DtoOutputDriver>();
        
        //Authentication
        CreateMap<bool, DtoOutputLogin>();
        CreateMap<bool, DtoOutputRegistration>();
    }
}