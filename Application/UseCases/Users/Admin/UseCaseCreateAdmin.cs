using Application.UseCases.Users.Admin.Dto;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Ef.Users.Admin;

namespace Application.UseCases.Users.Admin;

public class UseCaseCreateAdmin : IUseCaseWriter<DtoOutputAdmin, DtoInputCreateAdmin>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateAdmin(IMapper mapper, IAdminRepository adminRepository)
    {
        _mapper = mapper;
        _adminRepository = adminRepository;
    }

    public DtoOutputAdmin Execute(DtoInputCreateAdmin input)
    {
        var dbUser = _adminRepository.CreateAdmin(input.Username, input.Password, input.Email, input.Birthdate, input.IsBanned, input.PhoneNumber);
        return _mapper.Map<DtoOutputAdmin>(dbUser);
    }
}