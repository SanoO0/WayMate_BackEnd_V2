using System.Text.RegularExpressions;
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public UserType UserType { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    private string _birthdate;
    public bool IsBanned { get; set; }
    private string _phoneNumber;
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Gender { get; set; }
    public string? City { get; set; }
    
    public string Birthdate
    {
        get => _birthdate;
        set
        {
            if(Regex.IsMatch(value,@"^((0[1-9]|[1-2][0-9]|3[0-1])\/(0[1-9]|1[0-2])\/(19[0-9][0-9]|20[0-9][0-9]))$")) 
                _birthdate = value;
            else 
                throw new ArgumentException($"Date format not respected");
        }
    }

    public string PhoneNumber
    {
        get => _birthdate;
        set
        {
            if (Regex.IsMatch(value, @"^((((\+3[2-3]) )|0)4[0-9]{2})/[0-9]{2}\.[0-9]{2}\.[0-9]{2}$"))
                _phoneNumber = value;
            else
                throw new ArgumentException("Phone number format not respected for french and belgian numbers");
        }
    }

    public int CalculateAge(){
        //return DateTime.Today.Year - DateTime.Parse(Birthdate).Year;
        double age = DateTime.Today.Subtract(
            DateTime.Parse(Birthdate)
        ).TotalDays / 365.2425;
        return Convert.ToInt32(Math.Truncate(age));
    }
}