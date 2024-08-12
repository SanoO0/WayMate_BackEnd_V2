namespace Application.UseCases.Users.User.Dto;

public class DtoOutputPartialUser
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
}