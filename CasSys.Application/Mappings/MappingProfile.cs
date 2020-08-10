using AutoMapper;
using CasSys.Domain.Entities;
using CasSys.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using CasSys.Domain.Entities.Identity;
using CasSys.Application.RequestModels;

namespace CasSys.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<UserDto, AppUser>();
            CreateMap<UserUpdateRequestModel, AppUser>()
                .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null));

            CreateMap<IdentityRole, RoleDto>();

            CreateMap<JobRequestModel, Job>();
            CreateMap<Job, JobDto>();
            CreateMap<JobUpdateRequestModel, Job>()
                .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null));

            CreateMap<Applicant, ApplicantDto>();
        }
    }
}