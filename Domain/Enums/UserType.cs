using System.ComponentModel;

namespace Domain.Enums;

public enum UserType: int
{
    [Description("ADMIN")]
    Admin = 0,
        
    [Description("PASSENGER")]
    Passenger = 1,
    
    [Description("DRIVER")]
    Driver = 2,
    
    [Description("USER")]
    User = 3
}