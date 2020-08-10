using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.RequestModels;
using CasSys.WebApi.Infrastructure.Extensions;
using CasSys.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasSys.WebApi.Controllers
{
    [Authorize(Policy = "EmployeePolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IJobManagementService _jobService;

        public ApplicantController(IJobManagementService jobService)
        {
            this._jobService = jobService;
        }

        [HttpPost("job-apply")]
        [ValidateModel()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> JobApply(ApplicantRequestModel model)
        {
            var result = await _jobService.ApplyForJob(model, HttpContext.GetJwtUserResource());

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
