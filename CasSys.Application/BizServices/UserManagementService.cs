using AutoMapper;
using CasSys.Application.BizServices.Core;
using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.Dtos;
using CasSys.Application.RequestModels;
using CasSys.Domain.Entities.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using CasSys.Domain.EntityFrameworkCore.Collections;

namespace CasSys.Application.BizServices
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;

        public UserManagementService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._mapper = mapper;
        }

        public IEnumerable<UserDto> AllUsers => throw new System.NotImplementedException();

        public IEnumerable<RoleDto> AllRoles => throw new System.NotImplementedException();

        public UserDto CreateUser(UserRequestModel userModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _userManager.Users.Select(x => _mapper.Map<UserDto>(x));
        }

        public IPagedList<UserDto> GetUsers(int pageIndex, int pageSize)
        {
            return _userManager.Users.Select(x => _mapper.Map<UserDto>(x)).ToPagedList<UserDto>(pageIndex, pageIndex);
        }

        public async Task<OperationResult> CreateRoleAsync(RoleRequestModel roleModel)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleModel.Name
            });

            if (result.Succeeded)
            {
                return OperationResult.Success;
            }
            else
            {
                return OperationResult.Failed(GetErrors(result.Errors));
            }
        }

        private string[] GetErrors(IEnumerable<IdentityError> errors)
        {
            return errors.Select(x => x.Description).ToArray();
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return _roleManager.Roles.Select(x => _mapper.Map<RoleDto>(x));
        }

        public Task<RoleDto> GetRoleById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult<IPagedList<RoleDto>> GetRoles(int pageIndex, int pageSize)
        {
            var roles = _roleManager.Roles
                                .ToPagedList<IdentityRole, RoleDto>(converter: x => x.Select(y => _mapper.Map<RoleDto>(y)),
                                pageIndex: pageIndex, pageSize: pageSize);

            return OperationResult<IPagedList<RoleDto>>.Success(roles);
        }
    }
}