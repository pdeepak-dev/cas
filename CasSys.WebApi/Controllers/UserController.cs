using CasSys.Application.BizServices.Core;
using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.Dtos;
using CasSys.Application.RequestCommands;
using CasSys.Application.RequestModels;
using CasSys.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var users = _userManagementService.GetUsers();

            if (users == null)
                return NotFound();

            return Ok(OperationResult<IEnumerable<UserDto>>.Success(users));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromQuery] PaginatedRequestCommand cmd)
        {
            return Ok();
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

        [HttpPost("create-role")]
        [ValidateModel()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RoleRequestModel model)
        {
            var result = await _userManagementService.CreateRoleAsync(model);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }
    }
}