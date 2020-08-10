using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using CasSys.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using CasSys.Domain.Entities.Identity;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Core;
using CasSys.Application.BizServices.Interfaces;
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

        public IEnumerable<UserDto> AllUsers => GetUsers();

        public IEnumerable<RoleDto> AllRoles => GetRoles();

        public async Task<OperationResult> CreateUser(UserRequestModel userModel)
        {
            var user = new AppUser
            {
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Gender = userModel.Gender
            };

            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
                if (!userModel.IsEmployee)
                {
                    if (await _roleManager.RoleExistsAsync("Employeer"))
                    {
                        await _userManager.AddToRoleAsync(user, "Employeer");
                    }
                }
                else
                {
                    if (await _roleManager.RoleExistsAsync("Employee"))
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                    }
                }

                return OperationResult.Success;
            }
            else
                return OperationResult.Failed(GetErrors(result.Errors));
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

        public OperationResult<IPagedList<UserDto>> GetUsers(int pageIndex, int pageSize)
        {
            var users = _userManager.Users
                                .ToPagedList<AppUser, UserDto>(converter: x => x.Select(y => _mapper.Map<UserDto>(y)),
                                pageIndex: pageIndex, pageSize: pageSize);

            return OperationResult<IPagedList<UserDto>>.Success(users);
        }

        public async Task<OperationResult> CreateRoleAsync(RoleRequestModel roleModel)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleModel.Name
            });

            if (result.Succeeded)
                return OperationResult.Success;
            else
                return OperationResult.Failed(GetErrors(result.Errors));
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return _roleManager.Roles.Select(x => _mapper.Map<RoleDto>(x));
        }

        public OperationResult<IPagedList<RoleDto>> GetRoles(int pageIndex, int pageSize)
        {
            var roles = _roleManager.Roles
                                .ToPagedList<IdentityRole, RoleDto>(converter: x => x.Select(y => _mapper.Map<RoleDto>(y)),
                                pageIndex: pageIndex, pageSize: pageSize);

            return OperationResult<IPagedList<RoleDto>>.Success(roles);
        }

        public async Task<OperationResult> UpdateUser(UserUpdateRequestModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.UserId);

            if (user != null)
            {
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Gender = userModel.Gender;

                var result = _userManager.UpdateAsync(user);

                return OperationResult.Success;
            }

            return OperationResult.Failed(new[] { "User does not exists" });
        }

        // ----------------------------------------
        // private methods

        private string[] GetErrors(IEnumerable<IdentityError> errors)
        {
            return errors.Select(x => x.Description).ToArray();
        }
    }
}