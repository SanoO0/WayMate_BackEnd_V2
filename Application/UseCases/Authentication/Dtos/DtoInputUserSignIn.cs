using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Authentication.Dtos;

public class DtoInputUserSignIn
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public bool Logged { get; set; }
}