using AutoMapper;
using CasSys.Application.Dtos;
using CasSys.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace CasSys.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<UserDto, AppUser>();

            CreateMap<IdentityRole, RoleDto>();
        }
    }
}