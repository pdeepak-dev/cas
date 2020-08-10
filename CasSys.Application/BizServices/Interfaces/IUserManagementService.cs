using System.Threading.Tasks;
using CasSys.Application.Dtos;
using System.Collections.Generic;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Core;
using CasSys.Domain.EntityFrameworkCore.Collections;

namespace CasSys.Application.BizServices.Interfaces
{
    public interface IUserManagementService
    {
        #region User Service

        IEnumerable<UserDto> AllUsers { get; }
        IEnumerable<UserDto> GetUsers();
        Task<UserDto> GetUserById(string userId);

        OperationResult<IPagedList<UserDto>> GetUsers(int pageIndex, int pageSize);

        Task<OperationResult> CreateUser(UserRequestModel userModel);

        Task<OperationResult> UpdateUser(UserUpdateRequestModel userModel);

        Task<OperationResult> DeleteUser(UserBaseRequestModel userModel);

        IEnumerable<RoleDto> AllRoles { get; }
        IEnumerable<RoleDto> GetRoles();

        OperationResult<IPagedList<RoleDto>> GetRoles(int pageIndex, int pageSize);

        Task<OperationResult> CreateRoleAsync(RoleRequestModel roleModel);

        #endregion
    }
}