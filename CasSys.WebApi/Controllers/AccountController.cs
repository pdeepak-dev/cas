using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Interfaces;

namespace CasSys.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            this._authService = authService;
        }

        public async Task<IActionResult> Login(AuthenticateRequestModel model)
        {
            var result = await _authService.Validate(model);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }
    }
}