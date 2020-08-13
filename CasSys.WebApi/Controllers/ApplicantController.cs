using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.RequestModels;
using CasSys.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasSys.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IJobManagementService _jobService;

        public ApplicantController(IJobManagementService jobService)
        {
            this._jobService = jobService;
        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpPost("job-apply")]
        [ValidateModel()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> JobApply(ApplicantRequestModel model)
        {
            var result = await _jobService.ApplyForJob(model);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Policy = "EmployerPolicy")]
        [HttpGet("{jobId:int}/by-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicantsByJobId(int jobId)
        {
            var users = await _jobService.GetApplicantsByJobIdAsync(jobId);

            if (users?.Entity == null)
                return NotFound(users);

            return Ok(users);
        }
    }
}
