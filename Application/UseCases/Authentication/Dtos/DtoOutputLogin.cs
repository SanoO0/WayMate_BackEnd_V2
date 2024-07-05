namespace Application.UseCases.Authentication.Dtos;

public class DtoOutputLogin
{
    public bool isLogged { get; set; }
    public string username { get; set; }
    public string usertype { get; set; }
}