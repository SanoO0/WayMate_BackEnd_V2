using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Users.Driver.Dto;

public class DtoInputCreateDriver
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string Email { get; set; }
    [Required] public DateTime Birthdate { get; set; }
    [Required] public bool IsBanned { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string Gender { get; set; }
    [Required] public string City { get; set; }
}