﻿using AutoMapper;
using CasSys.Application.BizServices.Core;
using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.Dtos;
using CasSys.Application.RequestModels;
using CasSys.Domain.Entities.Identity;
using CasSys.Domain.EntityFrameworkCore.Collections;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    if (await _roleManager.RoleExistsAsync("Employer"))
                    {
                        await _userManager.AddToRoleAsync(user, "Employer");
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
                _mapper.Map<UserUpdateRequestModel, AppUser>(userModel, user);

                var result = await _userManager.UpdateAsync(user);

                return OperationResult.Success;
            }

            return OperationResult.Failed(new[] { "User does not exists" });
        }

        public async Task<OperationResult> DeleteUser(UserBaseRequestModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.UserId);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return OperationResult.Success;
                }
                else
                {
                    return OperationResult.Failed(GetErrors(result.Errors));
                }
            }

            return OperationResult.Failed(new[] { "User not found" });
        }

        public async Task<OperationResult<UserWithRoleDto>> GetUserWithRolesByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return OperationResult<UserWithRoleDto>.Success(new UserWithRoleDto
                {
                    User = _mapper.Map<UserDto>(user),
                    Roles = roles.Select(x => new RoleDto { Name = x, NormalizedName = x.ToUpper() })
                });
            }

            return OperationResult<UserWithRoleDto>.Failed(new[] { "User not found" });
        }

        // ----------------------------------------
        // private methods

        private string[] GetErrors(IEnumerable<IdentityError> errors)
        {
            return errors.Select(x => x.Description).ToArray();
        }
    }
}