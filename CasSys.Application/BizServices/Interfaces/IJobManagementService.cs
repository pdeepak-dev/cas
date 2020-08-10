using System.Threading.Tasks;
using CasSys.Application.Dtos;
using CasSys.Application.RequestModels;
using CasSys.Application.BizServices.Core;
using CasSys.Domain.EntityFrameworkCore.Collections;
using CasSys.Application.Jwt;

namespace CasSys.Application.BizServices.Interfaces
{
    public interface IJobManagementService
    {
        Task<OperationResult<IPagedList<JobDto>>> GetAllJobsAsync(int pageIndex, int pageSize);
        Task<OperationResult<JobDto>> GetJobByIdAsync(int id);

        Task<OperationResult<JobDto>> CreateJobAsync(JobRequestModel model, JwtUserResource jwt);
        Task<OperationResult> UpdateJobAsync(JobUpdateRequestModel model, JwtUserResource jwt);

        OperationResult DeleteJob(int id);
        OperationResult DeleteJob(JobBaseRequestModel model);

        OperationResult MarkJobAsFilled(int jobId);

        Task<OperationResult> ApplyForJob(ApplicantRequestModel model, JwtUserResource jwt);

        Task<OperationResult<JobApplicantDto>> GetApplicantsByJobIdAsync(int jobId);
    }
}