using CasSys.Application.BizServices.Interfaces;
using CasSys.Application.RequestCommands;
using CasSys.Application.RequestModels;
using CasSys.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace CasSys.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobManagementService _jobService;

        public JobController(IJobManagementService jobService)
        {
            this._jobService = jobService;
        }

        [HttpGet("{jobId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int jobId)
        {
            var users = await _jobService.GetJobByIdAsync(jobId);

            if (users?.Entity == null)
                return NotFound(users);

            return Ok(users);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedRequestCommand cmd)
        {
            var users = await _jobService.GetAllJobsAsync(cmd.Page, cmd.Take);

            if (users?.Entity?.Items == null)
                return NotFound(users);

            return Ok(users);
        }

        [HttpGet("user-specific-jobs")]
        [Authorize(Policy = "EmployeerPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllJobsByUserId([FromQuery] PaginatedRequestCommand cmd)
        {
            var users = await _jobService.GetAllJobsByUserIdAsync(cmd.Page, cmd.Take);

            if (users?.Entity?.Items == null)
                return NotFound(users);

            return Ok(users);
        }

        [HttpGet("{jobId:int}/applicants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicantsByJobId(int jobId)
        {
            var users = await _jobService.GetApplicantsByJobIdAsync(jobId);

            if (users?.Entity == null)
                return NotFound(users);

            return Ok(users);
        }

        [Authorize(Policy = "EmployeerPolicy")]
        [HttpPost]
        [ValidateModel()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] JobRequestModel model)
        {
            var result = await _jobService.CreateJobAsync(model);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Policy = "EmployeerPolicy")]
        [HttpPut]
        [ValidateModel()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(JobUpdateRequestModel model)
        {
            var result = await _jobService.UpdateJobAsync(model);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }

        [Authorize(Policy = "EmployeerPolicy")]
        [HttpPut("{jobId:int}/mark-fill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MarkJobAsFilled(int jobId)
        {
            var result = _jobService.MarkJobAsFilled(jobId);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }

        [Authorize(Policy = "EmployeerPolicy")]
        [HttpDelete("{jobId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int jobId)
        {
            var result = _jobService.DeleteJob(jobId);

            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest((object)result);
        }
    }
}