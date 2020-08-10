using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.RequestCommands;
using CasSys.Application.RequestModels;
using CasSys.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasSys.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserController(IUserManagementService userManagementService)
        {
            this._userManagementService = userManagementService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromQuery] PaginatedRequestCommand cmd)
        {
            var users = _userManagementService.GetUsers(cmd.Page, cmd.Take);

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet("roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRoles([FromQuery] PaginatedRequestCommand cmd)
        {
            var roles = _userManagementService.GetRoles(cmd.Page, cmd.Take);

            if (roles == null)
                return NotFound();

            return Ok(roles);
        }

        [HttpPut]
        [ValidateModel()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UserUpdateRequestModel model)
        {
            var result = await _userManagementService.UpdateUser(model);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(UserBaseRequestModel model)
        {
            var result = await _userManagementService.DeleteUser(model);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }

        [HttpPost("create-role")]
        [ValidateModel()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequestModel model)
        {
            var result = await _userManagementService.CreateRoleAsync(model);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }
    }
}