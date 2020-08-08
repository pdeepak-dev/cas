using System.Threading.Tasks;
using CasSys.Application.Dtos;
using System.Collections.Generic;
using CasSys.Application.RequestModels;
using CasSys.Domain.EntityFrameworkCore.Collections;
using Microsoft.AspNetCore.Identity;
using CasSys.Application.BizServices.Core;

namespace CasSys.Application.BizServices.Interfaces
{
    public interface IUserManagementService
    {
        #region User Service

        IEnumerable<UserDto> AllUsers { get; }
        IEnumerable<UserDto> GetUsers();
        Task<UserDto> GetUserById(string userId);

        IPagedList<UserDto> GetUsers(int pageIndex, int pageSize);

        UserDto CreateUser(UserRequestModel userResource);

        IEnumerable<RoleDto> AllRoles { get; }
        IEnumerable<RoleDto> GetRoles();
        Task<RoleDto> GetRoleById(string userId);

        OperationResult<IPagedList<RoleDto>> GetRoles(int pageIndex, int pageSize);

        Task<OperationResult> CreateRoleAsync(RoleRequestModel roleModel);

        #endregion
    }
}