using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CasSys.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserManagementService _userManagementService;

        public AccountController(IAuthService authService, IUserManagementService userManagementService)
        {
            this._authService = authService;
            this._userManagementService = userManagementService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserRequestModel model)
        {
            var result = await _userManagementService.CreateUser(model);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }
    }
}