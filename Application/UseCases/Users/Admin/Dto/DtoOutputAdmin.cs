namespace Application.UseCases.Users.Admin.Dto;

public class DtoOutputAdmin
{
    public int Id { get; set; }
    public string UserType { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsBanned { get; set; }
    public string PhoneNumber { get; set; }
}