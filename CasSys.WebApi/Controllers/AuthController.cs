﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CasSys.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(AuthenticateRequestModel authenticateRequestModel)
        {
            var result = await _authService.Authenticate(authenticateRequestModel);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return Unauthorized(result);
        }
    }
}