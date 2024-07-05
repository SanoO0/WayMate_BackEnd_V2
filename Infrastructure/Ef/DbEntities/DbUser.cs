namespace Infrastructure.Ef.DbEntities;

public class DbUser
{
    public int Id { get; set; }
    public string UserType { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsBanned { get; set; }
    public string PhoneNumber { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Gender { get; set; }
    public string City { get; set; }
}